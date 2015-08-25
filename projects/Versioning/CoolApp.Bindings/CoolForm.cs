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
using System.Security.Cryptography;
using System.Windows.Forms;
using DustInTheWind.Versioning;
using DustInTheWind.Versioning.WinForms.Mvp;
using DustInTheWind.Versioning.WinForms.Mvp.Config;
using DustInTheWind.Versioning.WinForms.Mvp.Versioning;

namespace DustInTheWind.CoolApp
{
    public partial class CoolForm : Form
    {
        private UserInterface userInterface;

        /// <summary>
        /// The default version file url. It is used only if the configuration object does not provide an url. 
        /// </summary>
        public const string DefaultCheckUrl = "http://azzul.alez.ro/appinfo.xml";

        public CoolForm()
        {
            InitializeComponent();

            userInterface = new UserInterface();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VersionCheckerForm form = new VersionCheckerForm();
            form.Owner = this;

            ICoolConfiguration coolConfiguration = new CoolConfiguration();
            coolConfiguration.Initialize();
            IConfigurationManager configurationManager = new ConfigurationManager(coolConfiguration);

            VersionChecker azzulVersionChecker = CreateVersionChecker(coolConfiguration);

            VersionCheckerViewModel viewModel = new VersionCheckerViewModel(userInterface, configurationManager, azzulVersionChecker);

            viewModel.View = form;
            form.ViewModel = viewModel;

            form.Show();
        }

        private VersionChecker CreateVersionChecker(ICoolConfiguration coolConfiguration)
        {
            return new VersionChecker
            {
                MinCheckTime = TimeSpan.FromSeconds(1),
                CurrentVersion = Version.Parse(textBoxAzzulVersion.Text),
                AppInfoProvider = new HttpAppVersionInfoProvider
                {
                    Url = GetRepositoryUrl(coolConfiguration),
                    AppName = "Azzul"
                }
            };
        }

        private static string GetRepositoryUrl(ICoolConfiguration coolConfiguration)
        {
            bool existsCustomUrl = coolConfiguration.CoolConfig != null && !string.IsNullOrEmpty(coolConfiguration.CoolConfig.Update.Url);
            return existsCustomUrl ? coolConfiguration.CoolConfig.Update.Url : DefaultCheckUrl;
        }
    }
}