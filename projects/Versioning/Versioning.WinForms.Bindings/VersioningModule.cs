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
using System.Configuration;
using System.Windows.Forms;
using DustInTheWind.Versioning.Check;
using DustInTheWind.Versioning.Config;
using DustInTheWind.Versioning.Download;
using DustInTheWind.Versioning.WinForms.Versioning;

namespace DustInTheWind.Versioning.WinForms
{
    public class VersioningModule
    {
        private readonly Version currentVersion;

        private readonly UserInterface userInterface;
        private readonly FileDownloader fileDownloader;

        /// <summary>
        /// The default version file url. It is used only if the configuration object does not provide an url. 
        /// </summary>
        public string DefaultCheckLocation { get; set; }

        public string AppWebPage { get; set; }

        public VersionChecker VersionChecker { get; private set; }

        public IVersionCheckerConfig Config { get; private set; }

        public VersioningModule(string appName, Version currentVersion, Configuration config)
        {
            if (appName == null) throw new ArgumentNullException("appName");
            if (currentVersion == null) throw new ArgumentNullException("currentVersion");
            if (config == null) throw new ArgumentNullException("config");

            this.currentVersion = currentVersion;

            DefaultCheckLocation = "http://azzul.alez.ro/appinfo.xml";
            Config = new VersionCheckerConfig(config);
            userInterface = new UserInterface();

            VersionChecker = CreateVersionCheckerForAzzul(appName);

            fileDownloader = new FileDownloader(userInterface);
        }

        private VersionChecker CreateVersionCheckerForAzzul(string appName)
        {
            return new VersionChecker
            {
                MinCheckTime = TimeSpan.FromSeconds(1),
                CurrentVersion = currentVersion,
                AppInfoProvider = new HttpAppInfoProvider
                {
                    Url = GetRepositoryUrl(),
                    AppName = appName
                }
            };
        }

        private string GetRepositoryUrl()
        {
            bool existsCustomUrl = !string.IsNullOrEmpty(Config.Url);
            return existsCustomUrl ? Config.Url : DefaultCheckLocation;
        }

        public void OpenVersionCheckerWindow(Form owner)
        {
            VersionCheckerForm form = new VersionCheckerForm { Owner = owner };

            VersionCheckerViewModel viewModel = new VersionCheckerViewModel(VersionChecker, fileDownloader, userInterface, Config)
            {
                AppWebPage = AppWebPage,
                View = form
            };

            form.ViewModel = viewModel;

            form.Show();
        }
    }
}