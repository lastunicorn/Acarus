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
using System.Windows.Forms;
using DustInTheWind.Versioning.WinForms;

namespace DustInTheWind.CoolApp
{
    public partial class CoolForm : Form
    {
        private readonly VersioningModule versioningModule;

        public CoolForm()
        {
            InitializeComponent();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            versioningModule = CreateVersioningModule(config);
            versioningModule.Config.CheckAtStartupChanged += HandleVersioningOptionsCheckAtStartupChanged;

            checkBoxCheckAtStartUp.Checked = versioningModule.Config.CheckAtStartup;
        }

        private VersioningModule CreateVersioningModule(Configuration config)
        {
            const string appName = "Azzul";
            Version currentVersion = Version.Parse(textBoxAzzulVersion.Text);

            return new VersioningModule(appName, currentVersion, config)
            {
                AppWebPage = "http://azzul.alez.ro",
                DefaultCheckLocation = "http://azzul.alez.ro/appinfo.xml"
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            versioningModule.OpenVersionCheckerWindow(this);
        }

        private void HandleVersioningOptionsCheckAtStartupChanged(object sender, EventArgs eventArgs)
        {
            checkBoxCheckAtStartUp.Checked = versioningModule.Config.CheckAtStartup;
        }

        private void checkBoxCheckAtStartUp_CheckedChanged(object sender, EventArgs e)
        {
            versioningModule.Config.CheckAtStartup = checkBoxCheckAtStartUp.Checked;
        }
    }
}