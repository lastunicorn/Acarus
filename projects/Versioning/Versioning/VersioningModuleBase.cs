using System;
using System.Configuration;
using DustInTheWind.Versioning.Check;
using DustInTheWind.Versioning.Config;
using DustInTheWind.Versioning.Download;

namespace DustInTheWind.Versioning
{
    public abstract class VersioningModuleBase
    {
        public VersionChecker Checker { get; private set; }
        protected IVersionCheckerConfig Config { get; set; }
        public IVersionCheckerUserInterface UserInterface { get; private set; }

        public bool CheckAtStartUp
        {
            get { return Config.CheckAtStartUp; }
            set
            {
                if (Config.CheckAtStartUp == value)
                    return;

                Config.CheckAtStartUp = value;
                OnCheckAtStartupChanged();
            }
        }

        public event EventHandler CheckAtStartUpChanged;

        protected VersioningModuleBase(Configuration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            Config = new VersionCheckerConfig(config);

            IUserInterface userInterface = CreateUserInterfaceHelper();

            Checker = new VersionChecker
            {
                MinCheckTime = TimeSpan.FromSeconds(1),
                AppInfoFileLocation = Config.Url,
                AppInfoProvider = new HttpFileProvider()
            };

            FileDownloader fileDownloader = new FileDownloader(userInterface);
            UserInterface = CreateVersionCheckerUserInterface(Checker, fileDownloader, userInterface, Config);

            Config.UrlChanged += HandleConfigUrlChanged;
        }

        protected abstract IUserInterface CreateUserInterfaceHelper();
        protected abstract IVersionCheckerUserInterface CreateVersionCheckerUserInterface(VersionChecker versionChecker, FileDownloader fileDownloader, IUserInterface userInterface, IVersionCheckerConfig versionCheckerConfig);

        private void HandleConfigUrlChanged(object sender, EventArgs eventArgs)
        {
            Checker.AppInfoFileLocation = Config.Url;
        }

        public void Start()
        {
            if (Config.CheckAtStartUp)
                Checker.CheckAsync();
        }

        protected virtual void OnCheckAtStartupChanged()
        {
            EventHandler handler = CheckAtStartUpChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}