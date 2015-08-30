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
using System.Windows.Forms;
using DustInTheWind.Versioning.Check;
using DustInTheWind.Versioning.Config;
using DustInTheWind.Versioning.Download;
using DustInTheWind.Versioning.WinForms.Versioning;

namespace DustInTheWind.Versioning.WinForms
{
    class VersionCheckerUi : IVersionCheckerUi
    {
        private readonly VersionChecker versionChecker;
        private readonly FileDownloader fileDownloader;
        private readonly UserInterface userInterface;
        private readonly IVersionCheckerConfig versionCheckerConfig;

        private VersionCheckerForm versionCheckerForm;

        public string AppWebPage { get; set; }

        public VersionCheckerUi(VersionChecker versionChecker, FileDownloader fileDownloader,
            UserInterface userInterface, IVersionCheckerConfig versionCheckerConfig)
        {
            if (versionChecker == null) throw new ArgumentNullException("versionChecker");
            if (fileDownloader == null) throw new ArgumentNullException("fileDownloader");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (versionCheckerConfig == null) throw new ArgumentNullException("versionCheckerConfig");

            this.versionChecker = versionChecker;
            this.fileDownloader = fileDownloader;
            this.userInterface = userInterface;
            this.versionCheckerConfig = versionCheckerConfig;
        }

        public void ShowVersionChecker(object owner)
        {
            if (versionCheckerForm != null)
            {
                versionCheckerForm.Activate();
            }
            else
            {
                versionCheckerForm = new VersionCheckerForm { Owner = owner as Form };
                versionCheckerForm.Closed += (sender, args) => versionCheckerForm = null;

                versionCheckerForm.ViewModel = new VersionCheckerViewModel(versionChecker, fileDownloader, userInterface, versionCheckerConfig, this);
                versionCheckerForm.ViewModel.AppWebPage = AppWebPage;

                versionCheckerForm.Show();
            }
        }

        public void CloseVersionChecker()
        {
            if (versionCheckerForm == null)
                return;

            versionCheckerForm.Close();
        }
    }
}