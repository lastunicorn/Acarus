// Azzul
// Copyright (C) 2009-2011 Dust in the Wind
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
using DustInTheWind.CoolApp.Config;
using DustInTheWind.CoolApp.Mvp.Window;
using DustInTheWind.CoolApp.Properties;
using DustInTheWind.Versioning;

namespace DustInTheWind.CoolApp.Versioning
{
    /// <summary>
    /// Presenter class that contains the logic of the Version Checker Window.
    /// </summary>
    internal class VersionCheckerPresenter : WindowPresenterBase<IVersionCheckerView>
    {
        /// <summary>
        /// The default url used if the configuration does not specify another one.
        /// </summary>
        private const string AzzulUrl = "http://azzul.alez.ro";

        /// <summary>
        /// Provides configuration values to be used in Azzul application.
        /// </summary>
        private readonly IConfigurationManager configurationManager;

        /// <summary>
        /// A service that checks if a newer version of Azzul exists.
        /// </summary>
        private readonly IAzzulVersionChecker azzulVersionChecker;

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
        /// Initializes a new instance of the <see cref="VersionCheckerPresenter"/> class.
        /// </summary>
        /// <param name="messagesService">A service that displays messages to the user.</param>
        /// <param name="configurationManager">Provides configuration values to be used in Azzul application.</param>
        /// <param name="azzulVersionChecker">A service that checks if a newer version of Azzul exists.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if one of the arguments is null.</exception>
        public VersionCheckerPresenter(IMessagesService messagesService, IConfigurationManager configurationManager,
            IAzzulVersionChecker azzulVersionChecker)
            : base(messagesService)
        {
            if (configurationManager == null) throw new ArgumentNullException("configurationManager");
            if (azzulVersionChecker == null) throw new ArgumentNullException("azzulVersionChecker");

            this.configurationManager = configurationManager;
            this.azzulVersionChecker = azzulVersionChecker;
        }

        #region Version Checker

        /// <summary>
        /// Call-back function called when the version checker finishes an asynchronous check.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCheckCompleted(object sender, CheckCompletedEventArgs e)
        {
            try
            {
                view.ProgressBarVisible = false;

                try
                {
                    if (e.Error != null)
                    {
                        versionCheckingResult = null;

                        view.StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_Error;
                        view.InformationText = e.Error.Message;

                        return;
                    }

                    versionCheckingResult = e.VersionCheckingResult;

                    DisplayVersionCheckingResult();
                }
                finally
                {
                    view.CheckAgainButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                messagesService.DisplayError(ex);
            }
        }

        private void DisplayVersionCheckingResult()
        {
            if (versionCheckingResult.IsNewerVersion)
            {
                DisplayVersionInformation(versionCheckingResult.RetrievedAppVersionInfo);
            }
            else
            {
                view.StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_NoNewVersion;
                view.InformationText = VersionCheckerResources.VersionCheckerWindow_NoNewVersion;
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
            view.ProgressBarValue = e.ProgressPercentage;

            string applicationName = versionCheckingResult.RetrievedAppVersionInfo.Name;
            string informationalVersion = versionCheckingResult.RetrievedAppVersionInfo.InformationalVersion;
            long kiloBytesReceived = e.BytesReceived / 1024;
            long totalKiloBytesToReceive = e.TotalBytesToReceive / 1024;

            view.InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Downloading, applicationName, informationalVersion, kiloBytesReceived, totalKiloBytesToReceive);
        }

        /// <summary>
        /// Call-back method called when the download is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                view.InformationText = VersionCheckerResources.VersionCheckerWindow_DownloadCanceled;

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
                    view.ProgressBarVisible = false;
                    view.InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_DownloadError, e.Error.Message);
                }
                else
                {
                    FileDownloadState fileDownloadState = e.UserState as FileDownloadState;
                    view.ProgressBarVisible = false;
                    if (fileDownloadState == null)
                    {
                        view.InformationText = VersionCheckerResources.VersionCheckerWindow_DownloadSuccess_Short;
                    }
                    else
                    {
                        view.InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_DownloadSuccess, fileDownloadState.DestinationFilePath);
                        downloadedFilePath = fileDownloadState.DestinationFilePath;
                        view.OpenDownloadedFileButtonVisible = true;
                    }
                }
            }

            view.CheckAgainButtonEnabled = true;
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
                ClearView();

                azzulVersionChecker.CheckCompleted += HandleCheckCompleted;

                fileDownloader = CreateFileDownloader();

                if (configurationManager == null)
                {
                    view.CheckAtStartupEnabled = false;
                    view.CheckAtStartupValue = false;
                }
                else
                {
                    view.CheckAtStartupEnabled = true;
                    view.CheckAtStartupValue = configurationManager.AzzulConfig.Update.CheckAtStartup;
                }

                // Check if already exists a version information.

                versionCheckingResult = azzulVersionChecker.LastCheckingResult;

                if (versionCheckingResult == null)
                    BeginCheck();
                else
                    DisplayVersionCheckingResult();
            }
            catch (Exception ex)
            {
                messagesService.DisplayError(ex);
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
                    {
                        fileDownloader.CancelAsync();
                    }
                }

                view.CloseView();
            }
            catch (Exception ex)
            {
                messagesService.DisplayError(ex);
            }
        }

        /// <summary>
        /// TestMe called by the view when the "Check Again" button is clicked.
        /// </summary>
        public void CheckAgainButtonClicked()
        {
            try
            {
                view.OpenDownloadedFileButtonVisible = false;
                BeginCheck();
            }
            catch (Exception ex)
            {
                messagesService.DisplayError(ex);
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
                        messagesService.DisplayMessage(VersionCheckerResources.VersionCheckerWindow_Error_AlreadyDownloading);
                        return;
                    }

                    string downloadUrl = versionCheckingResult.RetrievedAppVersionInfo.DownloadUrl;
                    Uri uri = new Uri(downloadUrl);
                    string urlFilePath = uri.AbsolutePath;
                    string fileName = Path.GetFileName(urlFilePath);

                    string directoryPath = view.RequestDirectory(null, VersionCheckerResources.VersionCheckerWindow_Download_SelectDestinationDirectory);

                    if (directoryPath == null)
                        return;

                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    string destinationFilePath = Path.Combine(directoryPath, fileName);
                    if (File.Exists(destinationFilePath))
                    {
                        string questionText = string.Format(VersionCheckerResources.VersionCheckerWindow_OverwriteQuestion, destinationFilePath);

                        if (view.AskOverwriteFile(questionText))
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
                messagesService.DisplayError(ex);
            }
        }

        /// <summary>
        /// TestMe called by the view when the "Check at startup" checkbox was clicked.
        /// </summary>
        public void CheckAtStartupCheckedChanged()
        {
            try
            {
                configurationManager.AzzulConfig.Update.CheckAtStartup = view.CheckAtStartupValue;
                configurationManager.Save();
            }
            catch (Exception ex)
            {
                messagesService.DisplayError(ex);
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
                messagesService.DisplayError(ex);
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Clears the view of all the displayed information.
        /// </summary>
        private void ClearView()
        {
            view.StatusText = string.Empty;
            view.ProgressBarVisible = false;
            view.DownloadButtonVisible = false;
            view.OpenDownloadedFileButtonVisible = false;
            view.InformationText = string.Empty;
            view.CheckAgainButtonEnabled = true;
            view.CheckAtStartupValue = false;
            view.CheckAtStartupEnabled = false;
        }

        /// <summary>
        /// Displayes in the view specified <see cref="VersionInformation"/> object.
        /// </summary>
        /// <param name="versionInformation">The <see cref="VersionInformation"/> object that will be displayed in the view.</param>
        private void DisplayVersionInformation(AppVersionInfo versionInformation)
        {
            view.ProgressBarVisible = false;

            view.StatusText = string.Format(VersionCheckerResources.VersionCheckerWindow_StatusText_NewVersionAvailable, versionInformation.InformationalVersion);

            if (versionInformation.DownloadUrl != null)
            {
                view.InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_VersionDescription, versionInformation.Description);
                view.DownloadButtonVisible = true;
            }
            else
            {
                view.InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_VersionDescriptionNoDownload, versionInformation.Description, AzzulUrl);
            }
        }

        /// <summary>
        /// Starts a new asynchronous check for new version.
        /// </summary>
        private void BeginCheck()
        {
            try
            {
                view.InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Checking, azzulVersionChecker.Url);
                view.ProgressBarVisible = true;
                view.ProgressBarType = ProgressBarType.Loading;
                view.DownloadButtonVisible = false;
                view.StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_Checking;
                view.CheckAgainButtonEnabled = false;
                versionCheckingResult = null;

                azzulVersionChecker.CheckAsync();
            }
            catch (Exception ex)
            {
                view.ProgressBarVisible = false;
                view.StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_ErrorChecking;
                view.InformationText = ex.Message;
                view.CheckAgainButtonEnabled = true;
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
                view.ProgressBarVisible = true;
                view.ProgressBarType = ProgressBarType.Percent;
                view.ProgressBarValue = 0;
                view.DownloadButtonVisible = false;
                view.InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Downloading, versionCheckingResult.RetrievedAppVersionInfo.Name, versionCheckingResult.RetrievedAppVersionInfo.InformationalVersion, 0, 0);
                view.CheckAgainButtonEnabled = false;

                FileDownloadState fileDownloadState = new FileDownloadState
                {
                    SourceUri = downloadUri,
                    DestinationFilePath = destinationFilePath
                };

                fileDownloader.DownloadFileAsync(downloadUri, destinationFilePath, fileDownloadState);
            }
            catch (Exception ex)
            {
                view.ProgressBarVisible = false;
                view.StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_DownloadError;
                view.InformationText = ex.Message;
                view.CheckAgainButtonEnabled = true;
            }
        }

        #endregion
    }
}
