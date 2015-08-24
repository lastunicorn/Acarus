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
using DustInTheWind.CoolApp.Properties;

namespace DustInTheWind.CoolApp.Versioning
{
    /// <summary>
    /// This form checks if there is a new version of the application on the web.
    /// </summary>
    internal partial class VersionCheckerForm : VersionCheckerFormBase, IVersionCheckerView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionCheckerForm"/> class.
        /// </summary>
        public VersionCheckerForm()
        {
            InitializeComponent();
        }

        #region Callback functions

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
        /// Callback function called when the form is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewVersionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Presenter.ViewWasClosed();
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

        #endregion

        /// <summary>
        /// Sets the visibility value of the progress bar.
        /// </summary>
        public bool ProgressBarVisible
        {
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new SetBoolValueDelegate(delegate(bool v) { ProgressBarVisible = v; }), new object[] {value});
                }
                else
                {
                    progressBar1.Visible = value;
                }
            }
        }

        /// <summary>
        /// Sets the mode in which the progress bar will work:
        /// Percent (will display a percentage);
        /// Loading (will display an infinite loading bar).
        /// </summary>
        public ProgressBarType ProgressBarType
        {
            set
            {
                switch (value)
                {
                    case ProgressBarType.Percent:
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        break;

                    default:
                        progressBar1.Style = ProgressBarStyle.Marquee;
                        break;
                }
            }
        }

        /// <summary>
        /// If the progress bar is in Percent mode, sets the percentage value displayed.
        /// </summary>
        public int ProgressBarValue
        {
            set { progressBar1.Value = value; }
        }

        /// <summary>
        /// Sets the visibility value of the "Download" button.
        /// </summary>
        public bool DownloadButtonVisible
        {
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new SetBoolValueDelegate(v => DownloadButtonVisible = v), new object[] {value});
                }
                else
                {
                    buttonDownload.Visible = value;
                }
            }
        }

        /// <summary>
        /// Sets the visibility value of the "Open Download Directory" button.
        /// </summary>
        public bool OpenDownloadedFileButtonVisible
        {
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new SetBoolValueDelegate(v => OpenDownloadedFileButtonVisible = v), new object[] {value});
                }
                else
                {
                    buttonOpenDownloadedFile.Visible = value;
                }
            }
        }

        /// <summary>
        /// Sets the the "Check Again" button's enable value.
        /// </summary>
        public bool CheckAgainButtonEnabled
        {
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new SetBoolValueDelegate(v => CheckAgainButtonEnabled = v), new object[] {value});
                }
                else
                {
                    buttonCheckAgain.Enabled = value;
                }
            }
        }

        /// <summary>
        /// Sets the status text displayed in the header of the view.
        /// </summary>
        public string StatusText
        {
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new SetStringValueDelegate(v => StatusText = v), new object[] {value});
                }
                else
                {
                    labelStatusText.Text = value;
                }
            }
        }

        /// <summary>
        /// Sets the information text displayed in the body of the view.
        /// </summary>
        public string InformationText
        {
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new SetStringValueDelegate(v => InformationText = v), new object[] {value});
                }
                else
                {
                    labelInfo.Text = value;
                }
            }
        }

        /// <summary>
        /// Requests a directory path from the user.
        /// </summary>
        /// <param name="initialPath">The initial path displayed as a suggestion to the user.</param>
        /// <param name="description">The description text to explain what will the directory path used for.</param>
        /// <returns>The directory path selected by the user or null if the user canceled the action.</returns>
        public string RequestDirectory(string initialPath, string description)
        {
            folderBrowserDialog1.SelectedPath = initialPath;
            folderBrowserDialog1.Description = description;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                return folderBrowserDialog1.SelectedPath;

            return null;
        }

        /// <summary>
        /// Gets or sets the checked value of the "Check at Startup" check box.
        /// </summary>
        public bool CheckAtStartupValue
        {
            get { return checkBoxCheckAtStartup.Checked; }
            set { checkBoxCheckAtStartup.Checked = value; }
        }

        /// <summary>
        /// Enables or desables the "Check at Startup" check box.
        /// </summary>
        public bool CheckAtStartupEnabled
        {
            set { checkBoxCheckAtStartup.Enabled = value; }
        }

        private delegate bool AskOverwriteFileDelegate(string message);

        /// <summary>
        /// Asks the user if he allows to overwrite the specified file.
        /// </summary>
        /// <param name="message">The question text to be displayed to the user..</param>
        /// <returns><c>true</c> if the user allows the file to be overwritten; <c>false</c> otherise.</returns>
        public bool AskOverwriteFile(string message)
        {
            if (InvokeRequired)
                return (bool) Invoke(new AskOverwriteFileDelegate(AskOverwriteFile), new object[] {message});

            return MessageBox.Show(this, message, VersionCheckerResources.VersionCheckerWindow_OverwriteQuestion_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}