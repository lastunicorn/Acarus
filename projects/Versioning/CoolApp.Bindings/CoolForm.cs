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
using System.Windows.Forms;
using DustInTheWind.Versioning.WinForms.Common;

namespace DustInTheWind.CoolApp
{
    partial class CoolForm : Form
    {
        private CoolViewModel viewModel;

        public CoolForm()
        {
            InitializeComponent();
        }

        public CoolViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != null)
                {
                    checkBoxCheckAtStartUp.DataBindings.Clear();
                    textBoxAzzulVersion.DataBindings.Clear();
                }

                viewModel = value;

                if (viewModel != null)
                {
                    checkBoxCheckAtStartUp.CreateBinding(x => x.Checked, viewModel, x => x.CheckAtStartUp, false, DataSourceUpdateMode.OnPropertyChanged);
                    textBoxAzzulVersion.CreateBinding(x => x.Text, viewModel, x => x.AzzulVersion, false, DataSourceUpdateMode.OnPropertyChanged);
                    toolStripStatusLabelNewVersion.CreateBinding(x => x.Text, viewModel, x => x.NewVersionText, false, DataSourceUpdateMode.Never);
                }
            }
        }

        private void HandleButtonCheckAzzulClick(object sender, EventArgs e)
        {
            viewModel.CheckAzzulButtonWasClicked(this);
        }
    }
}