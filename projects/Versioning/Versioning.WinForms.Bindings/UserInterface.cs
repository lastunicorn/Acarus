﻿// Acarus
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
using System.Threading;
using System.Windows.Forms;
using DustInTheWind.Versioning.WinForms.Properties;

namespace DustInTheWind.Versioning.WinForms
{
    /// <summary>
    /// Displays messages to the user.
    /// </summary>
    class UserInterface : IUserInterface
    {
        private readonly SynchronizationContext synchronizationContext;

        public Form MainWindow { get; set; }

        public UserInterface()
        {
            synchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Displays the exception in a friendly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        public virtual void DisplayError(Exception ex)
        {
            DisplayError(ex.Message);
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public virtual void DisplayError(string message)
        {
            MessageBox.Show(MainWindow, message, ServicesResources.MessagesService_Error_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public virtual void DisplayInfo(string message)
        {
            MessageBox.Show(MainWindow, message, ServicesResources.MessagesService_Information_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Asks the user a question in a message box and returns a yes/no answer.
        /// </summary>
        /// <param name="text">The question to be asked.</param>
        /// <param name="title">The text to be displayed in the title bar of the popup window.</param>
        /// <returns><c>true</c> if the user answered yes; <c>false</c> otherwise.</returns>
        public bool YesNoQuestion(string text, string title = null)
        {
            if (title == null)
                title = ServicesResources.MessagesService_Question_Title;

            DialogResult dialogResult = MessageBox.Show(MainWindow, text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// Requests a directory path from the user.
        /// </summary>
        /// <param name="initialPath">The initial path displayed as a suggestion to the user.</param>
        /// <param name="description">The description text to explain what will the directory path used for.</param>
        /// <returns>The directory path selected by the user or null if the user canceled the action.</returns>
        public string RequestDirectory(string initialPath, string description)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                SelectedPath = initialPath,
                Description = description
            };

            return dialog.ShowDialog() == DialogResult.OK
                ? dialog.SelectedPath
                : null;
        }

        /// <summary>
        /// Runs the specified action on the UI thread.
        /// </summary>
        /// <param name="action">The action to be run.</param>
        public void Dispatch(Action action)
        {
            synchronizationContext.Send(o => action(), null);
        }
    }
}