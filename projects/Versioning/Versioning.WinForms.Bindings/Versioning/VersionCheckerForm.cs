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

namespace DustInTheWind.Versioning.WinForms.Mvp.Versioning
{
    /// <summary>
    /// This form checks if there is a new version of the application on the web.
    /// </summary>
    public partial class VersionCheckerForm : Form, IVersionCheckerView
    {
        private VersionCheckerPresenter presenter;

        /// <summary>
        /// Gets or sets the Presenter that contains the logic of the <see cref="Form"/>.
        /// </summary>
        public VersionCheckerPresenter Presenter
        {
            get { return presenter; }
            set
            {
                if (presenter != null)
                    DetachDataSource();

                presenter = value;

                if (presenter != null)
                    AttachDataSource();
            }
        }

        private void DetachDataSource()
        {
            progressBar1.DataBindings.Clear();
            buttonDownload.DataBindings.Clear();
            buttonOpenDownloadedFile.DataBindings.Clear();
            buttonCheckAgain.DataBindings.Clear();
            labelStatusText.DataBindings.Clear();
            labelInfo.DataBindings.Clear();
            checkBoxCheckAtStartup.DataBindings.Clear();
        }

        private void AttachDataSource()
        {
            progressBar1.CreateBinding(x => x.Value, Presenter, x => x.ProgressBarValue, false, DataSourceUpdateMode.Never);
            progressBar1.CreateBinding(x => x.Visible, Presenter, x => x.ProgressBarVisible, false, DataSourceUpdateMode.Never);
            progressBar1.CreateBinding(x => x.Style, Presenter, x => x.ProgressBarStyle, false, DataSourceUpdateMode.Never);
            buttonDownload.CreateBinding(x => x.Visible, Presenter, x => x.DownloadButtonVisible, false, DataSourceUpdateMode.Never);
            buttonOpenDownloadedFile.CreateBinding(x => x.Visible, Presenter, x => x.OpenDownloadedFileButtonVisible, false, DataSourceUpdateMode.Never);
            buttonCheckAgain.CreateBinding(x => x.Enabled, Presenter, x => x.CheckAgainButtonEnabled, false, DataSourceUpdateMode.Never);
            labelStatusText.CreateBinding(x => x.Text, Presenter, x => x.StatusText, false, DataSourceUpdateMode.Never);
            labelInfo.CreateBinding(x => x.Text, Presenter, x => x.InformationText, false, DataSourceUpdateMode.Never);
            checkBoxCheckAtStartup.CreateBinding(x => x.Enabled, Presenter, x => x.CheckAtStartupEnabled, false, DataSourceUpdateMode.Never);
            checkBoxCheckAtStartup.CreateBinding(x => x.Checked, Presenter, x => x.CheckAtStartupValue, false, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionCheckerForm"/> class.
        /// </summary>
        public VersionCheckerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the view.
        /// </summary>
        public void CloseView()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(CloseView));
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Callback function called when the form is first displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewVersionForm_Shown(object sender, EventArgs e)
        {
            Presenter.ViewShown();
        }

        /// <summary>
        /// Callback function called when the "Close" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Presenter.CloseButtonClicked();
        }

        /// <summary>
        /// Callback function called when the "Check Again" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckAgain_Click(object sender, EventArgs e)
        {
            Presenter.CheckAgainButtonClicked();
        }

        /// <summary>
        /// Callback function called when the "Download" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            Presenter.DownloadButtonClicked();
        }

        /// <summary>
        /// Callback function called when the "Check At Startup" checkbox is checked or unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxCheckAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Binding binding in checkBoxCheckAtStartup.DataBindings)
                binding.WriteValue();

            Presenter.CheckAtStartupCheckedChanged();
        }

        /// <summary>
        /// Callback function called when the "Open Downloaded File" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenDownloadedFile_Click(object sender, EventArgs e)
        {
            Presenter.OpenDownloadedFileButtonClicked();
        }
    }
}