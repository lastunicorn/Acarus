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
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Versioning.Wpf;
using Versioning.Wpf.Common;

namespace DustInTheWind.CoolApp.Wpf
{
    class CoolViewModel : ViewModelBase
    {
        private readonly VersioningModule versioningModule;
        private bool checkAtStartUp;

        public bool CheckAtStartUp
        {
            get { return checkAtStartUp; }
            set
            {
                if(checkAtStartUp == value)
                    return;

                checkAtStartUp = value;
                OnPropertyChanged();

                versioningModule.Config.CheckAtStartup = value;
            }
        }

        public string AzzulVersion { get; set; }

        public ICommand CheckAzzulCommand { get; private set; }

        public CoolViewModel()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            AzzulVersion = "1.0.0.0";

            versioningModule = CreateVersioningModule(config);
            versioningModule.Config.CheckAtStartupChanged += HandleVersioningOptionsCheckAtStartupChanged;

            CheckAtStartUp = versioningModule.Config.CheckAtStartup;

            CheckAzzulCommand = new RelayCommand(p => true, CheckAzzul);
        }

        private VersioningModule CreateVersioningModule(Configuration config)
        {
            const string appName = "Azzul";
            Version currentVersion = Version.Parse(AzzulVersion);

            return new VersioningModule(appName, currentVersion, config)
            {
                AppWebPage = "http://azzul.alez.ro",
                DefaultCheckLocation = "http://azzul.alez.ro/appinfo.xml"
            };
        }

        private void CheckAzzul(object parameter)
        {
            versioningModule.OpenVersionCheckerWindow();
        }

        private void HandleVersioningOptionsCheckAtStartupChanged(object sender, EventArgs eventArgs)
        {
            CheckAtStartUp = versioningModule.Config.CheckAtStartup;
        }
    }

    internal class ViewModelBase : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
