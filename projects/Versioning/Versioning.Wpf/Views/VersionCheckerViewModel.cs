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
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows;
using DustInTheWind.Versioning;
using DustInTheWind.Versioning.Check;
using DustInTheWind.Versioning.Config;
using DustInTheWind.Versioning.Download;
using Versioning.Wpf.Common;
using Versioning.Wpf.Properties;

namespace Versioning.Wpf.Views
{
    class VersionCheckerViewModel : ViewModelBase
    {
        private readonly VersionChecker versionChecker;
        private readonly FileDownloader fileDownloader;
        private readonly UserInterface userInterface;
        private readonly IVersionCheckerConfig config;
        private readonly IVersionCheckerUserInterface versionCheckerUserInterface;

        private int progressBarValue;
        private Visibility progressBarVisible;
        private Visibility downloadButtonVisible;
        private Visibility openDownloadedFileButtonVisible;
        private bool checkAgainButtonEnabled;
        private string statusText;
        private string informationText;
        private bool checkAtStartupEnabled;
        private bool checkAtStartupValue;
        private bool progressBarStyle;
        private Image icon;

        public string AppWebPage { get; set; }

        public int ProgressBarValue
        {
            get { return progressBarValue; }
            private set
            {
                progressBarValue = value;
                OnPropertyChanged();
            }
        }

        public Visibility ProgressBarVisible
        {
            get { return progressBarVisible; }
            private set
            {
                progressBarVisible = value;
                OnPropertyChanged();
            }
        }

        public Visibility DownloadButtonVisible
        {
            get { return downloadButtonVisible; }
            private set
            {
                downloadButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public Visibility OpenDownloadedFileButtonVisible
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

        public bool ProgressBarStyle
        {
            get { return progressBarStyle; }
            set
            {
                progressBarStyle = value;
                OnPropertyChanged();
            }
        }

        public Image Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CheckAgainCommand { get; set; }
        public RelayCommand DownloadCommand { get; set; }
        public RelayCommand OpenDownloadedFileCommand { get; set; }
        public RelayCommand CheckAtStartupCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        public VersionCheckerViewModel(VersionChecker versionChecker, FileDownloader fileDownloader,
            UserInterface userInterface, IVersionCheckerConfig config, IVersionCheckerUserInterface versionCheckerUserInterface)
        {
            if (versionChecker == null) throw new ArgumentNullException("versionChecker");
            if (fileDownloader == null) throw new ArgumentNullException("fileDownloader");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (config == null) throw new ArgumentNullException("config");
            if (versionCheckerUserInterface == null) throw new ArgumentNullException("versionCheckerUserInterface");

            this.versionChecker = versionChecker;
            this.fileDownloader = fileDownloader;
            this.userInterface = userInterface;
            this.config = config;
            this.versionCheckerUserInterface = versionCheckerUserInterface;

            CheckAgainCommand = new RelayCommand(p => true, CheckAgainClicked);
            DownloadCommand = new RelayCommand(p => true, DownloadClicked);
            OpenDownloadedFileCommand = new RelayCommand(p => true, OpenDownloadedFileClicked);
            CheckAtStartupCommand = new RelayCommand(p => true, CheckAtStartupChanged);
            CloseCommand = new RelayCommand(p => true, CloseClicked);

            ChangeStateToEmpty();

            versionChecker.CheckStarting += HandleVersionCheckStarting;
            versionChecker.CheckCompleted += HandleCheckCompleted;

            this.fileDownloader.DownloadFileStarting += HandleDownloadFileStarting;
            this.fileDownloader.DownloadProgressChanged += HandleDownloadProgressChanged;
            this.fileDownloader.DownloadFileCompleted += HandleDownloadFileCompleted;

            config.CheckAtStartUpChanged += HandleOptionsCheckAtStartupChanged;

            CheckAtStartupEnabled = true;
            CheckAtStartupValue = config.CheckAtStartUp;
        }

        private void HandleOptionsCheckAtStartupChanged(object sender, EventArgs eventArgs)
        {
            ExecuteSafe(() =>
            {
                CheckAtStartupValue = config.CheckAtStartUp;
            });
        }

        private void HandleVersionCheckStarting(object sender, EventArgs eventArgs)
        {
            ExecuteSafe(ChangeStateToBeginVersionCheck);
        }

        private void HandleCheckCompleted(object sender, CheckCompletedEventArgs e)
        {
            ExecuteSafeInUi(() =>
            {
                if (e.Error != null)
                {
                    ChangeStateToVersionCheckError(e.Error);
                }
                else
                {
                    VersionCheckingResult checkingResult = versionChecker.LastCheckingResult;

                    if (checkingResult.IsNewerVersion)
                        ChangeStateToShowNewVersion(checkingResult);
                    else
                        ChangeStateToShowNoVersion();
                }
            });
        }

        private void HandleDownloadFileStarting(object sender, EventArgs e)
        {
            ExecuteSafeInUi(ChangeStateToBeginDownload);
        }

        private void HandleDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ExecuteSafeInUi(() =>
            {
                ProgressBarValue = e.ProgressPercentage;

                string applicationName = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.Name;
                string informationalVersion = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.InformationalVersion;
                long kiloBytesReceived = e.BytesReceived / 1024;
                long totalKiloBytesToReceive = e.TotalBytesToReceive / 1024;

                InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Downloading, applicationName, informationalVersion, kiloBytesReceived, totalKiloBytesToReceive);
            });
        }

        private void HandleDownloadFileCompleted(object sender, DownloadFileCompletedEventArgs e)
        {
            ExecuteSafeInUi(() =>
            {
                if (e.Cancelled)
                    ChangeStateToDownloadCanceled();
                else if (e.Error != null)
                    ChangeStateToDownloadError(e.Error);
                else
                    ChangeStateToDownloadSuccess();
            });
        }

        private void CheckAgainClicked(object obj)
        {
            ExecuteSafe(() =>
            {
                versionChecker.CheckAsync();
            });
        }

        private void DownloadClicked(object obj)
        {
            ExecuteSafe(() =>
            {
                if (versionChecker.LastCheckingResult == null ||
                   versionChecker.LastCheckingResult.RetrievedAppVersionInfo == null ||
                   versionChecker.LastCheckingResult.RetrievedAppVersionInfo.DownloadUrl == null)
                    return;

                string downloadUrl = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.DownloadUrl;

                fileDownloader.Download(downloadUrl);
            });
        }

        private void OpenDownloadedFileClicked(object obj)
        {
            ExecuteSafe(() =>
            {
                Process.Start(fileDownloader.DestinationFilePath);
            });
        }

        private void CheckAtStartupChanged(object obj)
        {
            ExecuteSafe(() =>
            {
                config.CheckAtStartUp = CheckAtStartupValue;
            });
        }

        private void CloseClicked(object obj)
        {
            ExecuteSafeInUi(() =>
            {
                versionChecker.Stop();
                fileDownloader.Stop();

                versionCheckerUserInterface.CloseVersionChecker();
            });
        }

        public void WindowWasLoaded()
        {
            ExecuteSafe(() =>
            {
                VersionCheckingResult lastCheckingResult = versionChecker.LastCheckingResult;

                if (lastCheckingResult == null)
                    versionChecker.CheckAsync();
                else if (lastCheckingResult.IsNewerVersion)
                    ChangeStateToShowNewVersion(lastCheckingResult);
                else
                    ChangeStateToShowNoVersion();
            });
        }

        private void ExecuteSafe(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        private void ExecuteSafeInUi(Action action)
        {
            userInterface.Dispatch(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    userInterface.DisplayError(ex);
                }
            });
        }

        private void ChangeStateToEmpty()
        {
            ProgressBarVisible = Visibility.Collapsed;

            StatusText = string.Empty;
            InformationText = string.Empty;

            DownloadButtonVisible = Visibility.Collapsed;
            OpenDownloadedFileButtonVisible = Visibility.Collapsed;
            CheckAgainButtonEnabled = true;
        }

        private void ChangeStateToBeginVersionCheck()
        {
            ProgressBarVisible = Visibility.Visible;
            ProgressBarStyle = true;

            string appInfoFileLocation = versionChecker.AppInfoFileLocation;

            InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Checking, appInfoFileLocation);
            StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_Checking;

            DownloadButtonVisible = Visibility.Collapsed;
            OpenDownloadedFileButtonVisible = Visibility.Collapsed;
            CheckAgainButtonEnabled = false;
        }

        private void ChangeStateToShowNoVersion()
        {
            ProgressBarVisible = Visibility.Collapsed;

            StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_NoNewVersion;
            InformationText = VersionCheckerResources.VersionCheckerWindow_NoNewVersion;

            DownloadButtonVisible = Visibility.Collapsed;
            CheckAgainButtonEnabled = true;
        }

        private void ChangeStateToShowNewVersion(VersionCheckingResult versionCheckingResult)
        {
            ProgressBarVisible = Visibility.Collapsed;

            AppVersionInfo versionInformation = versionCheckingResult.RetrievedAppVersionInfo;

            StatusText = string.Format(VersionCheckerResources.VersionCheckerWindow_StatusText_NewVersionAvailable, versionInformation.InformationalVersion);
            InformationText = versionInformation.DownloadUrl != null
                ? string.Format(VersionCheckerResources.VersionCheckerWindow_VersionDescription, versionInformation.Description)
                : string.Format(VersionCheckerResources.VersionCheckerWindow_VersionDescriptionNoDownload, versionInformation.Description, AppWebPage);

            DownloadButtonVisible = versionInformation.DownloadUrl != null ? Visibility.Visible : Visibility.Collapsed;
            CheckAgainButtonEnabled = true;
        }

        private void ChangeStateToVersionCheckError(Exception exception)
        {
            ProgressBarVisible = Visibility.Collapsed;

            StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_ErrorChecking;
            InformationText = exception.Message;

            DownloadButtonVisible = Visibility.Collapsed;
            CheckAgainButtonEnabled = true;
        }

        private void ChangeStateToBeginDownload()
        {
            ProgressBarVisible = Visibility.Visible;
            ProgressBarStyle = false;
            ProgressBarValue = 0;

            string appName = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.Name;
            string appVersion = versionChecker.LastCheckingResult.RetrievedAppVersionInfo.InformationalVersion;
            InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_Downloading, appName, appVersion, 0, 0);

            DownloadButtonVisible = Visibility.Collapsed;
            CheckAgainButtonEnabled = false;
        }

        private void ChangeStateToDownloadSuccess()
        {
            ProgressBarVisible = Visibility.Collapsed;

            InformationText = string.Format(VersionCheckerResources.VersionCheckerWindow_DownloadSuccess, fileDownloader.DestinationFilePath);

            OpenDownloadedFileButtonVisible = Visibility.Visible;
            DownloadButtonVisible = Visibility.Collapsed;
            CheckAgainButtonEnabled = true;
        }

        private void ChangeStateToDownloadCanceled()
        {
            ProgressBarVisible = Visibility.Collapsed;

            InformationText = VersionCheckerResources.VersionCheckerWindow_DownloadCanceled;

            DownloadButtonVisible = Visibility.Collapsed;
            CheckAgainButtonEnabled = true;
        }

        private void ChangeStateToDownloadError(Exception ex)
        {
            ProgressBarVisible = Visibility.Collapsed;

            StatusText = VersionCheckerResources.VersionCheckerWindow_StatusText_DownloadError;
            InformationText = ex.Message;

            DownloadButtonVisible = Visibility.Visible;
            CheckAgainButtonEnabled = true;
        }
    }
}
