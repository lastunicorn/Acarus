using System;
using System.Windows.Forms;
using DustInTheWind.CoolApp.Config;
using DustInTheWind.CoolApp.Versioning;
using DustInTheWind.Versioning;

namespace DustInTheWind.CoolApp
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// The default version file url. It is used only if the configuration object does not provide an url. 
        /// </summary>
        public const string DefaultCheckUrl = "http://azzul.alez.ro/appinfo.xml";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VersionCheckerForm form = new VersionCheckerForm();

            MessagesService messagesService = new MessagesService();

            IAzzulConfiguration azzulConfiguration = new AzzulConfiguration();
            azzulConfiguration.Initialize();
            IConfigurationManager configurationManager = new ConfigurationManager(azzulConfiguration);

            VersionChecker azzulVersionChecker = CreateVersionChecker(azzulConfiguration);

            VersionCheckerPresenter presenter = new VersionCheckerPresenter(messagesService, configurationManager, azzulVersionChecker);

            presenter.View = form;
            form.Presenter = presenter;

            presenter.ShowView();
        }

        private VersionChecker CreateVersionChecker(IAzzulConfiguration azzulConfiguration)
        {
            return new VersionChecker
            {
                MinCheckTime = TimeSpan.FromSeconds(1),
                CurrentVersion = Version.Parse(textBoxAzzulVersion.Text),
                AppInfoProvider = new HttpAppVersionInfoProvider
                {
                    Url = GetRepositoryUrl(azzulConfiguration),
                    AppName = "Azzul"
                }
            };
        }

        private static string GetRepositoryUrl(IAzzulConfiguration azzulConfiguration)
        {
            bool existsCustomUrl = azzulConfiguration.AzzulConfig != null && !string.IsNullOrEmpty(azzulConfiguration.AzzulConfig.Update.Url);
            return existsCustomUrl ? azzulConfiguration.AzzulConfig.Update.Url : DefaultCheckUrl;
        }
    }
}
