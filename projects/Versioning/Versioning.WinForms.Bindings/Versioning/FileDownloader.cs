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
using System.IO;
using System.Net;
using DustInTheWind.Versioning.WinForms.Mvp.Common;
using DustInTheWind.Versioning.WinForms.Mvp.Properties;

namespace DustInTheWind.Versioning.WinForms.Mvp.Versioning
{
    internal class FileDownloader
    {
        private readonly IUserInterface userInterface;
        private readonly WebClient webClient;
        private Uri downloadUri;
        private string destinationFilePath;

        /// <summary>
        /// Object used to synchronize the access to the file downloader.
        /// </summary>
        private readonly object downloaderLock = new object();

        public event DownloadProgressChangedEventHandler DownloadProgressChanged
        {
            add { webClient.DownloadProgressChanged += value; }
            remove { webClient.DownloadProgressChanged -= value; }
        }

        public event EventHandler DownloadFileStarting;
        public event EventHandler<DownloadFileCompletedEventArgs> DownloadFileCompleted;

        public FileDownloader(IUserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;

            webClient = new WebClient();
            webClient.DownloadFileCompleted += HandleDownloadFileCompleted;
        }

        public string DownloadedFilePath
        {
            get { return destinationFilePath; }
        }

        private void HandleDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (File.Exists(destinationFilePath))
                {
                    try
                    {
                        File.Delete(destinationFilePath);
                    }
                    catch
                    {
                        // ignored
                    }
                }

                DownloadFileCompletedEventArgs eva = new DownloadFileCompletedEventArgs(true);
                OnDownloadFileCompleted(eva);
            }
            else
            {
                DownloadFileCompletedEventArgs eva = new DownloadFileCompletedEventArgs();
                OnDownloadFileCompleted(eva);
            }
        }

        protected virtual void OnDownloadFileCompleted(DownloadFileCompletedEventArgs e)
        {
            EventHandler<DownloadFileCompletedEventArgs> handler = DownloadFileCompleted;

            if (handler != null)
                handler(this, e);
        }

        public void Download(string downloadUrl)
        {
            lock (downloaderLock)
            {
                if (webClient.IsBusy)
                {
                    userInterface.DisplayInfo(VersionCheckerResources.VersionCheckerWindow_Error_AlreadyDownloading);
                    return;
                }

                Uri uri = new Uri(downloadUrl);
                string urlFilePath = uri.AbsolutePath;
                string fileName = Path.GetFileName(urlFilePath);

                string destinationDirectoryPath = userInterface.RequestDirectory(null, VersionCheckerResources.VersionCheckerWindow_Download_SelectDestinationDirectory);

                if (destinationDirectoryPath == null)
                    return;

                if (!Directory.Exists(destinationDirectoryPath))
                    Directory.CreateDirectory(destinationDirectoryPath);

                string destinationFilePath = Path.Combine(destinationDirectoryPath, fileName);

                if (File.Exists(destinationFilePath))
                {
                    bool shouldOverwrite = AskToOverwrite(destinationFilePath);

                    if (shouldOverwrite)
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

        private bool AskToOverwrite(string destinationFilePath)
        {
            string questionText = string.Format(VersionCheckerResources.VersionCheckerWindow_OverwriteQuestion, destinationFilePath);
            string title = VersionCheckerResources.VersionCheckerWindow_OverwriteQuestion_Title;

            return userInterface.YesNoQuestion(questionText, title);
        }

        private void BeginDownload(Uri downloadUri, string destinationFilePath)
        {
            try
            {
                OnDownloadFileStarting();

                this.downloadUri = downloadUri;
                this.destinationFilePath = destinationFilePath;

                webClient.DownloadFileAsync(downloadUri, destinationFilePath);
            }
            catch (Exception ex)
            {
                DownloadFileCompletedEventArgs eva = new DownloadFileCompletedEventArgs(ex);
                OnDownloadFileCompleted(eva);
            }
        }

        public void Stop()
        {
            lock (downloaderLock)
            {
                if (webClient.IsBusy)
                    webClient.CancelAsync();
            }
        }

        protected virtual void OnDownloadFileStarting()
        {
            EventHandler handler = DownloadFileStarting;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}