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
        /// <summary>
        /// The time when the last asynchronous check was started. If no asynchronous check was performed yet this value is null.
        /// </summary>
        private DateTime? checkStartTime;

        /// <summary>
        /// Object used to synchronize the asynchronous calls.
        /// </summary>
        private readonly object checkSyncObject = new object();

        /// <summary>
        /// Gets a value specifying if an asynchronous operation is in progress.
        /// </summary>
        public bool IsBusy { get; private set; }

        /// <summary>
        /// The minimum time that an asynchronous check should take.
        /// If the operation is finished to soon, the thread is blocked until the specified time passed.
        /// </summary>
        private TimeSpan? minCheckTime;

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
                lock (checkSyncObject)
                {
                    if (IsBusy)
                        throw new InvalidOperationException(VersioningResources.InternalError_MinCheckTimeCannotBeSet);

                    minCheckTime = value;
                }
            }
        }

        public Version CurrentVersion { get; set; }

        public IFileProvider AppInfoProvider { get; set; }

        public string AppName { get; set; }

        public string AppInfoFileLocation { get; set; }

        /// <summary>
        /// Gets the result of the latest performed check.
        /// </summary>
        public VersionCheckingResult LastCheckingResult { get; private set; }

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
            if (!ChangeStateToBusy())
                return false;

            OnCheckStarting();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    CheckInternal();

                    // If the asynchronous check finishes to quickly, simulate it takes a little longer.
                    DelayAsyncCheck();

                    OnCheckCompleted(new CheckCompletedEventArgs(LastCheckingResult));
                }
                catch (VersionCheckingException ex)
                {
                    OnCheckCompleted(new CheckCompletedEventArgs(ex));
                }
                catch (Exception ex)
                {
                    OnCheckCompleted(new CheckCompletedEventArgs(new VersionCheckingException(ex)));
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
            lock (checkSyncObject)
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
            lock (checkSyncObject)
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

            lock (checkSyncObject)
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