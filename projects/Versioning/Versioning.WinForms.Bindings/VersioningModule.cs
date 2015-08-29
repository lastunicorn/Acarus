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
using DustInTheWind.Versioning.Check;
using DustInTheWind.Versioning.Config;
using DustInTheWind.Versioning.Download;
using DustInTheWind.Versioning.WinForms.Versioning;

namespace DustInTheWind.Versioning.WinForms
{
    public class VersioningModule
    {
        private readonly FileDownloader fileDownloader;
        private readonly UserInterface userInterface;

        public string AppWebPage { get; set; }

        public VersionChecker VersionChecker { get; private set; }

        public IVersionCheckerConfig VersionCheckerConfig { get; private set; }

        public VersioningModule(Configuration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            VersionCheckerConfig = new VersionCheckerConfig(config);
            userInterface = new UserInterface();

            VersionChecker = new VersionChecker
            {
                MinCheckTime = TimeSpan.FromSeconds(1),
                AppInfoFileLocation = VersionCheckerConfig.Url,
                AppInfoProvider = new HttpFileProvider()
            };

            fileDownloader = new FileDownloader(userInterface);

            VersionCheckerConfig.UrlChanged += HandleConfigUrlChanged;

            userInterface.ViewModelCreator = ViewModelCreator;
        }

        private VersionCheckerViewModel ViewModelCreator()
        {
            return new VersionCheckerViewModel(VersionChecker, fileDownloader, userInterface, VersionCheckerConfig)
            {
                AppWebPage = AppWebPage
            };
        }

        private void HandleConfigUrlChanged(object sender, EventArgs eventArgs)
        {
            VersionChecker.AppInfoFileLocation = VersionCheckerConfig.Url;
        }

        public void OpenVersionCheckerWindow(object owner)
        {
            userInterface.ShowVersionChecker(owner);
        }
    }
}