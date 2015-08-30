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

namespace DustInTheWind.Versioning.WinForms
{
    public class VersioningModule
    {
        public VersionChecker Checker { get; private set; }

        public IVersionCheckerConfig Config { get; private set; }

        public IVersionCheckerUi UserInterface { get; private set; }

        public VersioningModule(Configuration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            Config = new VersionCheckerConfig(config);

            UserInterface userInterface = new UserInterface();

            Checker = new VersionChecker
            {
                MinCheckTime = TimeSpan.FromSeconds(1),
                AppInfoFileLocation = Config.Url,
                AppInfoProvider = new HttpFileProvider()
            };

            FileDownloader fileDownloader = new FileDownloader(userInterface);

            UserInterface = new VersionCheckerUi(Checker, fileDownloader, userInterface, Config);

            Config.UrlChanged += HandleConfigUrlChanged;
        }

        private void HandleConfigUrlChanged(object sender, EventArgs eventArgs)
        {
            Checker.AppInfoFileLocation = Config.Url;
        }
    }
}