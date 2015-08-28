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

        public CoolViewModel()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            versioningModule = new VersioningModule(config)
            {
                AppWebPage = "http://azzul.alez.ro"
            };

            if (string.IsNullOrEmpty(versioningModule.VersionCheckerConfig.Url))
                versioningModule.VersionCheckerConfig.Url = "http://azzul.alez.ro/appinfo.xml";

            versioningModule.VersionCheckerConfig.CheckAtStartUpChanged += HandleVersioningOptionsCheckAtStartUpChanged;

            AzzulVersion = "1.0.0.0";
            CheckAtStartUp = versioningModule.VersionCheckerConfig.CheckAtStartUp;

            versioningModule.VersionChecker.AppName = "Azzul";
            versioningModule.VersionChecker.CurrentVersion = Version.Parse(AzzulVersion);
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

                versioningModule.VersionCheckerConfig.CheckAtStartUp = checkAtStartUp;
            }
        }

        public string AzzulVersion
        {
            get { return azzulVersion; }
            set
            {
                if (azzulVersion == value)
                    return;

                azzulVersion = value;
                OnPropertyChanged();

                Version version;

                if (Version.TryParse(value, out version))
                    versioningModule.VersionChecker.CurrentVersion = version;
            }
        }

        private void HandleVersioningOptionsCheckAtStartUpChanged(object sender, EventArgs eventArgs)
        {
            CheckAtStartUp = versioningModule.VersionCheckerConfig.CheckAtStartUp;
        }

        public void OpenVersionCheckerWindow(CoolForm coolForm)
        {
            versioningModule.OpenVersionCheckerWindow(coolForm);
        }
    }
}