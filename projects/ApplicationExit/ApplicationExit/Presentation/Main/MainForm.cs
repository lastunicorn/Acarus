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

using System.Windows.Forms;

namespace ApplicationExit.Presentation.Main
{
    partial class MainForm : Form
    {
        private MainViewModel viewModel;

        public MainViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != null)
                {
                    theDataView.ViewModel = null;
                    buttonExit.ViewModel = null;
                    buttonSave.ViewModel = null;
                    buttonChange.ViewModel = null;
                }

                viewModel = value;

                if (viewModel != null)
                {
                    theDataView.ViewModel = viewModel.TheDataModel;
                    buttonExit.ViewModel = viewModel.ExitButtonModel;
                    buttonSave.ViewModel = viewModel.SaveButtonModel;
                    buttonChange.ViewModel = viewModel.ChangeButtonModel;
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !viewModel.FormIsClosing();
        }

        private void buttonExit_Click(object sender, System.EventArgs e)
        {

        }
    }
}