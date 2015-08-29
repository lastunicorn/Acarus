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
using DustInTheWind.Versioning.WinForms;
using DustInTheWind.Versioning.WinForms.Common;

namespace DustInTheWind.CoolApp
{
    internal class CoolViewModel : ViewModelBase
    {
        private readonly VersioningModule versioningModule;

        private string azzulVersion;
        private bool checkAtStartUp;

        public string AzzulVersion
        {
            get { return azzulVersion; }
            set
            {
                if (azzulVersion == value)
                    return;

                azzulVersion = value;
                OnPropertyChanged();

                if (!IsInitializing)
                {
                    Version version;

                    if (Version.TryParse(value, out version))
                        versioningModule.VersionChecker.CurrentVersion = version;
                }
            }
        }

        public bool CheckAtStartUp
        {
            get { return checkAtStartUp; }
            set
            {
                if (checkAtStartUp == value)
                    return;

                checkAtStartUp = value;
                OnPropertyChanged();

                if (!IsInitializing)
                    versioningModule.VersionCheckerConfig.CheckAtStartUp = checkAtStartUp;
            }
        }

        public CoolViewModel()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            versioningModule = new AzzulVersioningModule(config) { AppWebPage = "http://azzul.alez.ro" };

            versioningModule.VersionCheckerConfig.CheckAtStartUpChanged += HandleVersioningOptionsCheckAtStartUpChanged;

            Initialize(() =>
            {
                AzzulVersion = versioningModule.VersionChecker.CurrentVersion.ToString();
                CheckAtStartUp = versioningModule.VersionCheckerConfig.CheckAtStartUp;
            });
        }

        private void HandleVersioningOptionsCheckAtStartUpChanged(object sender, EventArgs eventArgs)
        {
            CheckAtStartUp = versioningModule.VersionCheckerConfig.CheckAtStartUp;
        }

        public void CheckAzzulButtonWasClicked(object coolForm)
        {
            versioningModule.OpenVersionCheckerWindow(coolForm);
        }
    }
}