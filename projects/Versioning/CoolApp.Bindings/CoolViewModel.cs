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
using DustInTheWind.CoolApp.Utils;
using DustInTheWind.Versioning.Check;
using DustInTheWind.Versioning.WinForms;
using DustInTheWind.Versioning.WinForms.Common;

namespace DustInTheWind.CoolApp
{
    internal class CoolViewModel : ViewModelBase
    {
        private readonly UserInterface userInterface;
        private readonly VersioningModule versioningModule;

        private string azzulVersion;
        private bool checkAtStartUp;
        private string newVersionText;

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
                        versioningModule.Checker.CurrentVersion = version;
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
                    versioningModule.Config.CheckAtStartUp = checkAtStartUp;
            }
        }

        public string NewVersionText
        {
            get { return newVersionText; }
            set
            {
                newVersionText = value;
                OnPropertyChanged();
            }
        }

        public CoolViewModel(UserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            versioningModule = new AzzulVersioningModule(config);
            versioningModule.UserInterface.AppWebPage = "http://azzul.alez.ro";

            Initialize(() =>
            {
                AzzulVersion = versioningModule.Checker.CurrentVersion.ToString();
                CheckAtStartUp = versioningModule.Config.CheckAtStartUp;
            });

            versioningModule.Config.CheckAtStartUpChanged += HandleVersioningOptionsCheckAtStartUpChanged;
            versioningModule.Checker.CheckCompleted += HandleVersionCheckerCheckCompleted;
        }

        private void HandleVersionCheckerCheckCompleted(object sender, CheckCompletedEventArgs e)
        {
            ExecuteSafeInUi(() =>
            {
                if (e.Cancelled || e.Error != null)
                    return;

                NewVersionText = e.VersionCheckingResult.IsNewerVersion
                    ? "New version: " + e.VersionCheckingResult.RetrievedAppVersionInfo.Version
                    : "No new version";
            });
        }

        private void HandleVersioningOptionsCheckAtStartUpChanged(object sender, EventArgs e)
        {
            CheckAtStartUp = versioningModule.Config.CheckAtStartUp;
        }

        public void CheckAzzulButtonWasClicked(object coolForm)
        {
            versioningModule.UserInterface.ShowVersionChecker(coolForm);
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
    }
}