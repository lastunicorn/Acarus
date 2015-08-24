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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.CoolApp.Properties;

namespace DustInTheWind.CoolApp
{
    /// <summary>
    /// Displays messages to the user.
    /// </summary>
    internal class MessagesService : IMessagesService
    {
        public Form MainWindow { get; set; }

        /// <summary>
        /// Displays the exception in a friendly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        public virtual void DisplayError(Exception ex)
        {
            DisplayErrorMessage(ex.Message);
        }

        /// <summary>
        /// Displays the exception in a friendly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        /// <param name="message">The message text to be displayed along with the error message.</param>
        public void DisplayError(Exception ex, string message)
        {
            string text = string.IsNullOrEmpty(message) ? ex.Message : message + "\n" + ex.Message;
            DisplayErrorMessage(text);
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public virtual void DisplayErrorMessage(string message)
        {
            ShowMessageBox(MainWindow, message, ServicesResources.MessagesService_Error_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public virtual void DisplayMessage(string message)
        {
            ShowMessageBox(MainWindow, message, ServicesResources.MessagesService_Information_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays a list of warnings to the user.
        /// </summary>
        /// <param name="warnings">The list of warnings to be displayed to the user.</param>
        public void DisplayWarnings(Exception[] warnings)
        {
            string message = BuildMessage(warnings);

            ShowMessageBox(MainWindow, message, ServicesResources.MessagesService_Warning_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static string BuildMessage(IEnumerable<Exception> warnings)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Exception warning in warnings)
                sb.AppendLine(warning.Message);

            return sb.ToString();
        }

        private void ShowMessageBox(Form parentForm, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (parentForm != null)
            {
                if (parentForm.InvokeRequired)
                {
                    object[] parameters = new object[] {parentForm, message, title, buttons, icon};
                    parentForm.Invoke(new Action<Form, string, string, MessageBoxButtons, MessageBoxIcon>(ShowMessageBox), parameters);
                }
                else
                {
                    MessageBox.Show(parentForm, message, title, buttons, icon);
                }
            }
            else
            {
                MessageBox.Show(message, title, buttons, icon);
            }
        }

        /// <summary>
        /// Asks the user a question in a message box and returns a yes/no answer.
        /// </summary>
        /// <param name="text">The question to be asked.</param>
        /// <returns><c>true</c> if the user answered yes; <c>false</c> otherwise.</returns>
        public bool YesNoQuestion(string text)
        {
            return MessageBox.Show(MainWindow, text, ServicesResources.MessagesService_Question_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        /// <summary>
        /// Presents the user a warning and asks a question in a message box and returns a yes/no answer.
        /// </summary>
        /// <param name="text">The warning and question to be asked.</param>
        /// <returns><c>true</c> if the user answered yes; <c>false</c> otherwise.</returns>
        public bool YesNoWarning(string text)
        {
            return MessageBox.Show(MainWindow, text, ServicesResources.MessagesService_Warning_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        public bool? YesNoCancelQuestion(string text)
        {
            DialogResult result = MessageBox.Show(MainWindow, text, ServicesResources.MessagesService_Question_Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (result)
            {
                default:
                case DialogResult.Cancel:
                    return null;

                case DialogResult.No:
                    return false;

                case DialogResult.Yes:
                    return true;
            }
        }
    }
}