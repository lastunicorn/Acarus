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
using DustInTheWind.Versioning.Properties;

namespace DustInTheWind.Versioning.Download
{
    public class FileDownloader : IDisposable
    {
        private readonly IUserInterface userInterface;
        private readonly WebClient webClient;

        public FileDownloadResult LastDownloadResult { get; private set; }
        
        private Uri sourceUri;

        private bool isDisposed;

        /// <summary>
        /// Object used to synchronize the access to the file downloader.
        /// </summary>
        private readonly object downloaderLock = new object();

        public string DestinationFilePath { get; private set; }

        public event EventHandler DownloadFileStarting;

        public event DownloadProgressChangedEventHandler DownloadProgressChanged
        {
            add { webClient.DownloadProgressChanged += value; }
            remove { webClient.DownloadProgressChanged -= value; }
        }

        public event EventHandler<DownloadFileCompletedEventArgs> DownloadFileCompleted;

        public FileDownloader(IUserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;

            webClient = new WebClient();
            webClient.DownloadFileCompleted += HandleDownloadFileCompleted;
        }

        private void HandleDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (File.Exists(DestinationFilePath))
                {
                    try
                    {
                        File.Delete(DestinationFilePath);
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

        public void Download(string downloadUrl)
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            lock (downloaderLock)
            {
                if (webClient.IsBusy)
                {
                    userInterface.DisplayInfo(VersionCheckerResources.VersionCheckerWindow_Error_AlreadyDownloading);
                    return;
                }

                LastDownloadResult = null;

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

                sourceUri = downloadUri;
                DestinationFilePath = destinationFilePath;

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

        protected virtual void OnDownloadFileCompleted(DownloadFileCompletedEventArgs e)
        {
            EventHandler<DownloadFileCompletedEventArgs> handler = DownloadFileCompleted;

            if (handler != null)
                handler(this, e);
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            webClient.Dispose();

            isDisposed = true;
        }
    }
}