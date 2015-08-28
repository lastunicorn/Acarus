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
            VersionCheckerConfigurationSection configSection = Config.ConfigSection;

            bool existsCustomUrl = !string.IsNullOrEmpty(configSection.Url);
            return existsCustomUrl ? configSection.Url : DefaultCheckLocation;
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