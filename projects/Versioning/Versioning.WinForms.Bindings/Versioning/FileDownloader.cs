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

namespace DustInTheWind.Versioning.WinForms.Mvp.Versioning
{
    internal class FileDownloader
    {
        private readonly WebClient webClient;
        private Uri downloadUri;
        private string destinationFilePath;

        public event DownloadProgressChangedEventHandler DownloadProgressChanged
        {
            add { webClient.DownloadProgressChanged += value; }
            remove { webClient.DownloadProgressChanged -= value; }
        }

        public event AsyncCompletedEventHandler DownloadFileCompleted;

        public FileDownloader()
        {
            webClient = new WebClient();
            webClient.DownloadFileCompleted += HandleDownloadFileCompleted;
        }

        public bool IsBusy
        {
            get { return webClient.IsBusy; }
        }

        public string DownloadedFilePath
        {
            get { return destinationFilePath; }
        }

        public void Cancel()
        {
            webClient.CancelAsync();
        }

        public void DownloadFileAsync(Uri downloadUri, string destinationFilePath)
        {
            this.downloadUri = downloadUri;
            this.destinationFilePath = destinationFilePath;

            webClient.DownloadFileAsync(downloadUri, destinationFilePath);
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
                if (File.Exists(destinationFilePath))
                {
                    try
                    {
                        File.Delete(destinationFilePath);
                    }
                    catch
                    {
                    }
                }
            }

            OnDownloadFileCompleted(e);
        }

        protected virtual void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
        {
            AsyncCompletedEventHandler handler = DownloadFileCompleted;

            if (handler != null)
                handler(this, e);
        }
    }
}