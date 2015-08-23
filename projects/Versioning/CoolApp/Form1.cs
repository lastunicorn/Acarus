using System;
using System.Windows.Forms;
using DustInTheWind.CoolApp.Config;
using DustInTheWind.CoolApp.Services;
using DustInTheWind.CoolApp.Versioning;

namespace DustInTheWind.CoolApp
{
    public partial class Form1 : Form
    {
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
            IApplicationService applicationService = new ApplicationService();
            AzzulVersionChecker azzulVersionChecker = new AzzulVersionChecker(azzulConfiguration, applicationService);
            VersionCheckerPresenter presenter = new VersionCheckerPresenter(messagesService, configurationManager, azzulVersionChecker);

            presenter.View = form;
            form.Presenter = presenter;

            presenter.ShowView();
        }
    }
}
