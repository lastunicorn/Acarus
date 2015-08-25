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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using DustInTheWind.Versioning.WinForms.Mvp.Common;
using DustInTheWind.Versioning.WinForms.Mvp.Config;
using DustInTheWind.Versioning.WinForms.Mvp.Properties;

namespace DustInTheWind.Versioning.WinForms.Mvp.Versioning
{
    /// <summary>
    /// ViewModel class that contains the logic of the Version Checker Window.
    /// </summary>
    public class VersionCheckerViewModel : ViewModelBase
    {
        private int progressBarValue;
        private bool progressBarVisible;
        private bool downloadButtonVisible;
        private bool openDownloadedFileButtonVisible;
        private bool checkAgainButtonEnabled;
        private string statusText;
        private string informationText;
        private bool checkAtStartupEnabled;
        private bool checkAtStartupValue;
        private ProgressBarStyle progressBarStyle;

        /// <summary>
        /// Gets or sets the view that represents the GUI.
        /// Once the view is set it cannot be changed.
        /// </summary>
        /// <exception cref="CoolException">Thrown if the view is already set.</exception>
        public IVersionCheckerView View { get; set; }

        /// <summary>
        /// The default url used if the configuration does not specify another one.
        /// </summary>
        private const string CoolAppUrl = "http://azzul.alez.ro";

        /// <summary>
        /// Provides configuration values to be used in Azzul application.
        /// </summary>
        private readonly ICoolConfiguration coolConfiguration;

        /// <summary>
        /// A service that checks if a newer version of Azzul exists.
        /// </summary>
        private readonly VersionChecker versionChecker;

        /// <summary>
        /// The <see cref="WebClient"/> used to download files.
        /// </summary>
        private readonly FileDownloader fileDownloader;

        /// <summary>
        /// Object used to synchronize the access to the file downloader.
        /// </summary>
        private readonly object downloaderLock = new object();

        /// <summary>
        /// A service that displays messages to the user.
        /// </summary>
        private readonly IUserInterface userInterface;

        public int ProgressBarValue
        {
            get { return progressBarValue; }
            private set
            {
                progressBarValue = value;
                OnPropertyChanged();
            }
        }

        public bool ProgressBarVisible
        {
            get { return progressBarVisible; }
            private set
            {
                progressBarVisible = value;
                OnPropertyChanged();
            }
        }

        public bool DownloadButtonVisible
        {
            get { return downloadButtonVisible; }
            private set
            {
                downloadButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public bool OpenDownloadedFileButtonVisible
        {
            get { return openDownloadedFileButtonVisible; }
            private set
            {
                openDownloadedFileButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public bool CheckAgainButtonEnabled
        {
            get { return checkAgainButtonEnabled; }
            private set
            {
                checkAgainButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public string StatusText
        {
            get { return statusText; }
            private set
            {
                statusText = value;
                OnPropertyChanged();
            }
        }

        public string InformationText
        {
            get { return informationText; }
            private set
            {
                informationText = value;
                OnPropertyChanged();
            }
        }

        public bool CheckAtStartupEnabled
        {
            get { return checkAtStartupEnabled; }
            private set
            {
                checkAtStartupEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool CheckAtStartupValue
        {
            get { return checkAtStartupValue; }
            set
            {
                checkAtStartupValue = value;
                OnPropertyChanged();
            }
        }

        public ProgressBarStyle ProgressBarStyle
        {
            get { return progressBarStyle; }
            set
            {
                progressBarStyle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionCheckerViewModel"/> class.
        /// </summary>
        /// <param name="userInterface">A service that displays messages to the user.</param>
        /// <param name="coolConfiguration">Provides configuration values to be used in the application.</param>
        /// <param name="versionChecker">A service that checks if a newer version of the application exists.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if one of the arguments is null.</exception>
        public VersionCheckerViewModel(IUserInterface userInterface, ICoolConfiguration coolConfiguration, VersionChecker versionChecker)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (coolConfiguration == null) throw new ArgumentNullException("coolConfiguration");
            if (versionChecker == null) throw new ArgumentNullException("versionChecker");

            this.userInterface = userInterface;
            this.coolConfiguration = coolConfiguration;
            this.versionChecker = versionChecker;

            StatusText = string.Empty;
            InformationText = string.Empty;
            CheckAgainButtonEnabled = true;

            versionChecker.CheckCompleted += HandleCheckCompleted;

            fileDownloader = new FileDownloader();
            fileDownloader.DownloadProgressChanged += HandleDownloadProgressChanged;
            fileDownloader.DownloadFileCompleted += HandleDownloadFileCompleted;

            CheckAtStartupEnabled = true;
            CheckAtStartupValue = coolConfiguration.CoolConfig.Update.CheckAtStartup;
        }

        #region Version Checker

        /// <summary>
        /// Call-back function called when the version checker finishes an asynchronous check.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCheckCompleted(object sender, CheckCompletedEventArgs e)
        {
            userInterface.Dispatch(() =>
            {
                try
                {
                    ProgressBarVisible = false;

                    try
                    {
                        if (e.Error != null)
                        {
                            StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_Error;
                            InformationText = e.Error.Message;

                            return;
                        }

                        DisplayVersionCheckingResult();
                    }
                    finally
                    {
                        CheckAgainButtonEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    userInterface.DisplayError(ex);
                }
            });
        }

        private void DisplayVersionCheckingResult()
        {
            if (versionChecker.LastCheckingResult.IsNewerVersion)
            {
                DisplayVersionInformation(versionChecker.LastCheckingResult.RetrievedAppVersionInfo);
            }
            else
            {
                StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_NoNewVersion;
                InformationText = VersionCheckerResources.VersionCheckerWindow_NoNewVersion;
            }
        }

        #endregion

        #region File Downloader

        /// <summary>
        /// Call-back method called when the progress of the download is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBarValue = e.ProgressPercentage;

            string applicationName = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.Name;
            string informationalVersion = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.InformationalVersion;
            long kiloBytesReceived = e.BytesReceived / 1024;
            long totalKiloBytesToReceive = e.TotalBytesToReceive / 1024;

            InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Downloading, applicationName, informationalVersion, kiloBytesReceived, totalKiloBytesToReceive);
        }

        /// <summary>
        /// Call-back method called when the download is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            userInterface.Dispatch(() =>
            {
                if (e.Cancelled)
                {
                    InformationText = VersionCheckerResources.VersionCheckerWindow_DownloadCanceled;
                }
                else
                {
                    if (e.Error != null)
                    {
                        ProgressBarVisible = false;
                        InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_DownloadError, e.Error.Message);
                    }
                    else
                    {
                        ProgressBarVisible = false;

                        InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_DownloadSuccess, fileDownloader.DownloadedFilePath);
                        OpenDownloadedFileButtonVisible = true;
                    }
                }

                CheckAgainButtonEnabled = true;
            });
        }

        #endregion

        #region View's Actions

        /// <summary>
        /// TestMe called by the view when the view is first shown.
        /// </summary>
        public void ViewShown()
        {
            try
            {
                if (versionChecker.LastCheckingResult == null)
                    BeginCheck();
                else
                    DisplayVersionCheckingResult();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// TestMe called by the view when the "Close" button is clicked.
        /// </summary>
        public void CloseButtonClicked()
        {
            try
            {
                lock (downloaderLock)
                {
                    if (fileDownloader.IsBusy)
                        fileDownloader.Cancel();
                }

                View.CloseView();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// TestMe called by the view when the "Check Again" button is clicked.
        /// </summary>
        public void CheckAgainButtonClicked()
        {
            try
            {
                OpenDownloadedFileButtonVisible = false;
                BeginCheck();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// TestMe called by the view when the "Download" button was clicked.
        /// </summary>
        public void DownloadButtonClicked()
        {
            try
            {
                lock (downloaderLock)
                {
                    if (fileDownloader.IsBusy)
                    {
                        userInterface.DisplayInfo(VersionCheckerResources.VersionCheckerWindow_Error_AlreadyDownloading);
                        return;
                    }

                    string downloadUrl = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.DownloadUrl;
                    Uri uri = new Uri(downloadUrl);
                    string urlFilePath = uri.AbsolutePath;
                    string fileName = Path.GetFileName(urlFilePath);

                    string directoryPath = userInterface.RequestDirectory(null, VersionCheckerResources.VersionCheckerWindow_Download_SelectDestinationDirectory);

                    if (directoryPath == null)
                        return;

                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    string destinationFilePath = Path.Combine(directoryPath, fileName);
                    if (File.Exists(destinationFilePath))
                    {
                        string questionText = string.Format(VersionCheckerResources.VersionCheckerWindow_OverwriteQuestion, destinationFilePath);
                        string title = VersionCheckerResources.VersionCheckerWindow_OverwriteQuestion_Title;

                        if (userInterface.YesNoQuestion(questionText, title))
                        {
                            File.Delete(destinationFilePath);
                            BeginDownload(uri, destinationFilePath);
                        }
                    }
                    else
                    {
                        BeginDownload(uri, destinationFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// TestMe called by the view when the "Check at startup" checkbox was clicked.
        /// </summary>
        public void CheckAtStartupCheckedChanged()
        {
            try
            {
                coolConfiguration.CoolConfig.Update.CheckAtStartup = CheckAtStartupValue;
                coolConfiguration.Save();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        /// <summary>
        /// TestMe called by the view when the "Open Downloaded File" button is clicked.
        /// </summary>
        public void OpenDownloadedFileButtonClicked()
        {
            try
            {
                Process.Start(fileDownloader.DownloadedFilePath);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Displayes in the view specified <see cref="AppVersionInfo"/> object.
        /// </summary>
        /// <param name="versionInformation">The <see cref="AppVersionInfo"/> object that will be displayed in the view.</param>
        private void DisplayVersionInformation(AppVersionInfo versionInformation)
        {
            ProgressBarVisible = false;

            StatusText = string.Format(VersionCheckerResources.VersionCheckerWindow_StatusText_NewVersionAvailable, versionInformation.InformationalVersion);

            if (versionInformation.DownloadUrl != null)
            {
                InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_VersionDescription, versionInformation.Description);
                DownloadButtonVisible = true;
            }
            else
            {
                InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_VersionDescriptionNoDownload, versionInformation.Description, CoolAppUrl);
            }
        }

        /// <summary>
        /// Starts a new asynchronous check for new version.
        /// </summary>
        private void BeginCheck()
        {
            try
            {
                string location = versionChecker.AppInfoProvider == null
                    ? null
                    : versionChecker.AppInfoProvider.Location;

                InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Checking, location);
                ProgressBarVisible = true;
                ProgressBarStyle = ProgressBarStyle.Marquee;
                DownloadButtonVisible = false;
                StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_Checking;
                CheckAgainButtonEnabled = false;

                versionChecker.CheckAsync();
            }
            catch (Exception ex)
            {
                ProgressBarVisible = false;
                StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_ErrorChecking;
                InformationText = ex.Message;
                CheckAgainButtonEnabled = true;
            }
        }

        /// <summary>
        /// Starts a new asynchronous download.
        /// </summary>
        /// <param name="downloadUri">The url of the file that has to be downloaded.</param>
        /// <param name="destinationFilePath">The full name and path of the file that will be saved locally.</param>
        private void BeginDownload(Uri downloadUri, string destinationFilePath)
        {
            try
            {
                ProgressBarVisible = true;
                ProgressBarStyle = ProgressBarStyle.Blocks;
                ProgressBarValue = 0;
                DownloadButtonVisible = false;
                string appName = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.Name;
                string appVersion = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.InformationalVersion;
                InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Downloading, appName, appVersion, 0, 0);
                CheckAgainButtonEnabled = false;

                fileDownloader.DownloadFileAsync(downloadUri, destinationFilePath);
            }
            catch (Exception ex)
            {
                ProgressBarVisible = false;
                StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_DownloadError;
                InformationText = ex.Message;
                CheckAgainButtonEnabled = true;
            }
        }

        #endregion
    }
}