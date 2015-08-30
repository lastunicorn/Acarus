// Acarus
// Copyright (C) 2015 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.Versioning.Properties;

namespace DustInTheWind.Versioning.Check
{
    /// <summary>
    /// Checks a reference version (considered the current version) against the version obtained from a <see cref="IFileProvider"/> object.
    /// It has synchronous and asynchronous methods.
    /// </summary>
    public class VersionChecker
    {
        private readonly object synchronizationObject = new object();

        private DateTime? checkStartTime;
        private volatile bool isBusy;
        private TimeSpan? minCheckTime;
        private Version currentVersion;
        private IFileProvider appInfoProvider;
        private string appName;
        private string appInfoFileLocation;
        private volatile VersionCheckingResult lastCheckingResult;

        /// <summary>
        /// Gets a value specifying if an asynchronous operation is in progress.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            private set { isBusy = value; }
        }

        /// <summary>
        /// Gets or sets the minimum time that an asynchronous check should take.
        /// If the operation is finished to soon, the thread is blocked until the specified time passed.
        /// </summary>
        /// <exception cref="InvalidOperationException">The asynchronous check is already running.</exception>
        public TimeSpan? MinCheckTime
        {
            get { return minCheckTime; }
            set
            {
                if (IsBusy)
                    throw new InvalidOperationException(VersioningResources.InternalError_MinCheckTimeCannotBeSet);

                minCheckTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the current version of the application for which the current instance will check the version.
        /// </summary>
        public Version CurrentVersion
        {
            get { return currentVersion; }
            set
            {
                if (IsBusy)
                    throw new InvalidOperationException("You are not allowed to change the current version while the version checker is running.");

                currentVersion = value;
            }
        }

        public IFileProvider AppInfoProvider
        {
            get { return appInfoProvider; }
            set
            {
                if (IsBusy)
                    throw new InvalidOperationException("You are not allowed to change the app info provider while the version checker is running.");

                appInfoProvider = value;
            }
        }

        public string AppName
        {
            get { return appName; }
            set
            {
                if (IsBusy)
                    throw new InvalidOperationException("You are not allowed to change the app name while the version checker is running.");

                appName = value;
            }
        }

        public string AppInfoFileLocation
        {
            get { return appInfoFileLocation; }
            set
            {
                if (IsBusy)
                    throw new InvalidOperationException("You are not allowed to change the app info file location while the version checker is running.");

                appInfoFileLocation = value;
            }
        }

        /// <summary>
        /// Gets the result of the latest performed check.
        /// </summary>
        public VersionCheckingResult LastCheckingResult
        {
            get { return lastCheckingResult; }
            private set { lastCheckingResult = value; }
        }

        /// <summary>
        /// Event raised before the asynchronous check is started.
        /// </summary>
        public event EventHandler<EventArgs> CheckStarting;

        /// <summary>
        /// Event raised when the version checker finishes an asynchronous check.
        /// </summary>
        public event EventHandler<CheckCompletedEventArgs> CheckCompleted;

        /// <summary>
        /// Asynchronously compares the version obtained from the version provider with the current one.
        /// If another asynchronous check is in progress, nothing is done.
        /// </summary>
        /// <returns><c>true</c> if the asynchronous check was successfully started; <c>false</c> if another asynchronous check is already running.</returns>
        public bool CheckAsync()
        {
            if (CurrentVersion == null) throw new VerificationException("CurrentVersion was not set.");
            if (AppName == null) throw new VerificationException("AppName was not set.");
            if (AppInfoProvider == null) throw new VerificationException("AppInfoProvider was not set.");
            if (AppInfoFileLocation == null) throw new VerificationException("AppInfoFileLocation was not set.");

            if (!ChangeStateToBusy())
                return false;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    CheckInternal();
                }
                finally
                {
                    ChangeStateBackToReady();
                }
            });

            return true;
        }

        private void CheckInternal()
        {
            OnCheckStarting();

            try
            {
                RetrieveNewCheckingResult();

                // If the asynchronous check finishes to quickly, simulate it takes a little longer.
                DelayAsyncCheck();
            }
            catch (VersionCheckingException ex)
            {
                OnCheckCompleted(new CheckCompletedEventArgs(ex));
                return;
            }
            catch (Exception ex)
            {
                OnCheckCompleted(new CheckCompletedEventArgs(new VersionCheckingException(ex)));
                return;
            }

            OnCheckCompleted(new CheckCompletedEventArgs(LastCheckingResult));
        }

        private void RetrieveNewCheckingResult()
        {
            LastCheckingResult = null;

            using (Stream stream = AppInfoProvider.GetStream(AppInfoFileLocation))
            {
                AppVersionInfo newestVersion = AppInfoFileParser.GetAppInfo(stream, AppName);

                int comparationResult = newestVersion.Version.CompareTo(CurrentVersion);

                LastCheckingResult = new VersionCheckingResult
                {
                    CurrentVersion = CurrentVersion,
                    RetrievedAppVersionInfo = newestVersion,
                    ComparationResult = comparationResult
                };
            }
        }

        private bool ChangeStateToBusy()
        {
            lock (synchronizationObject)
            {
                if (IsBusy)
                    return false;

                checkStartTime = DateTime.Now;
                IsBusy = true;
            }

            return true;
        }

        private void ChangeStateBackToReady()
        {
            lock (synchronizationObject)
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// If necessary (<see cref="minCheckTime"/> is different then null), delays the asynchronous check
        /// to reach the minimum running time specified by <see cref="minCheckTime"/> field.
        /// </summary>
        private void DelayAsyncCheck()
        {
            TimeSpan waitTime = TimeSpan.Zero;

            lock (synchronizationObject)
            {
                if (minCheckTime != null && checkStartTime != null)
                {
                    DateTime checkEndTime = DateTime.Now;
                    TimeSpan checkTime = checkEndTime - checkStartTime.Value;

                    if (checkTime < minCheckTime)
                        waitTime = minCheckTime.Value - checkTime;
                }
            }

            if (waitTime != TimeSpan.Zero)
                Thread.Sleep(waitTime);
        }

        public void Stop()
        {
            // todo: implement this
        }

        protected virtual void OnCheckCompleted(CheckCompletedEventArgs e)
        {
            EventHandler<CheckCompletedEventArgs> handler = CheckCompleted;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnCheckStarting()
        {
            EventHandler<EventArgs> handler = CheckStarting;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}