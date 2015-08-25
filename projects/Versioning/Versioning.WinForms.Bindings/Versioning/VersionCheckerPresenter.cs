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
using DustInTheWind.Versioning.WinForms.Mvp.Config;
using DustInTheWind.Versioning.WinForms.Mvp.Properties;

namespace DustInTheWind.Versioning.WinForms.Mvp.Versioning
{
    /// <summary>
    /// Presenter class that contains the logic of the Version Checker Window.
    /// </summary>
    public class VersionCheckerPresenter : ViewModelBase
    {
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
        private readonly IConfigurationManager configurationManager;

        /// <summary>
        /// A service that checks if a newer version of Azzul exists.
        /// </summary>
        private readonly VersionChecker versionChecker;

        /// <summary>
        /// The information about the newest version.
        /// This value can be set from the outside of the form or by the version checker.
        /// </summary>
        private VersionCheckingResult versionCheckingResult;

        /// <summary>
        /// The full name, with path, of the file that was downloaded.
        /// </summary>
        private string downloadedFilePath;

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
        /// Initializes a new instance of the <see cref="VersionCheckerPresenter"/> class.
        /// </summary>
        /// <param name="userInterface">A service that displays messages to the user.</param>
        /// <param name="configurationManager">Provides configuration values to be used in the application.</param>
        /// <param name="versionChecker">A service that checks if a newer version of the application exists.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if one of the arguments is null.</exception>
        public VersionCheckerPresenter(IUserInterface userInterface, IConfigurationManager configurationManager, VersionChecker versionChecker)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (configurationManager == null) throw new ArgumentNullException("configurationManager");
            if (versionChecker == null) throw new ArgumentNullException("versionChecker");
            
            this.userInterface = userInterface;
            this.configurationManager = configurationManager;
            this.versionChecker = versionChecker;

            StatusText = string.Empty;
            InformationText = string.Empty;
            CheckAgainButtonEnabled = true;

            versionCheckingResult = versionChecker.LastCheckingResult;
            versionChecker.CheckCompleted += HandleCheckCompleted;

            fileDownloader = CreateFileDownloader();

            CheckAtStartupEnabled = true;
            CheckAtStartupValue = configurationManager.CoolConfig.Update.CheckAtStartup;
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
                            versionCheckingResult = null;

                            StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_Error;
                            InformationText = e.Error.Message;

                            return;
                        }

                        versionCheckingResult = e.VersionCheckingResult;

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
            if (versionCheckingResult.IsNewerVersion)
            {
                DisplayVersionInformation(versionCheckingResult.RetrievedAppVersionInfo);
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
        /// The <see cref="WebClient"/> used to download files.
        /// </summary>
        private WebClient fileDownloader;

        /// <summary>
        /// Object used to synchronize the access to the file downloader.
        /// </summary>
        private readonly object downloaderLock = new object();

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
        /// Creates a new version checker object.
        /// </summary>
        private WebClient CreateFileDownloader()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += HandleDownloadProgressChanged;
            webClient.DownloadFileCompleted += HandleDownloadFileCompleted;
            return webClient;
        }

        /// <summary>
        /// Call-back method called when the progress of the download is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBarValue = e.ProgressPercentage;

            string applicationName = versionCheckingResult.RetrievedAppVersionInfo.Name;
            string informationalVersion = versionCheckingResult.RetrievedAppVersionInfo.InformationalVersion;
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

                    FileDownloadState downloadState = e.UserState as FileDownloadState;
                    if (downloadState != null)
                    {
                        if (File.Exists(downloadState.DestinationFilePath))
                        {
                            try
                            {
                                File.Delete(downloadState.DestinationFilePath);
                            }
                            catch
                            {
                            }
                        }
                    }
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
                        FileDownloadState fileDownloadState = e.UserState as FileDownloadState;
                        ProgressBarVisible = false;
                        if (fileDownloadState == null)
                        {
                            InformationText = VersionCheckerResources.VersionCheckerWindow_DownloadSuccess_Short;
                        }
                        else
                        {
                            InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_DownloadSuccess, fileDownloadState.DestinationFilePath);
                            downloadedFilePath = fileDownloadState.DestinationFilePath;
                            OpenDownloadedFileButtonVisible = true;
                        }
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
                if (versionCheckingResult == null)
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
                        fileDownloader.CancelAsync();
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

                    string downloadUrl = versionCheckingResult.RetrievedAppVersionInfo.DownloadUrl;
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
                configurationManager.CoolConfig.Update.CheckAtStartup = CheckAtStartupValue;
                configurationManager.Save();
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
                Process.Start(downloadedFilePath);
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
                versionCheckingResult = null;

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
                InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Downloading, versionCheckingResult.RetrievedAppVersionInfo.Name, versionCheckingResult.RetrievedAppVersionInfo.InformationalVersion, 0, 0);
                CheckAgainButtonEnabled = false;

                FileDownloadState fileDownloadState = new FileDownloadState
                {
                    SourceUri = downloadUri,
                    DestinationFilePath = destinationFilePath
                };

                fileDownloader.DownloadFileAsync(downloadUri, destinationFilePath, fileDownloadState);
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