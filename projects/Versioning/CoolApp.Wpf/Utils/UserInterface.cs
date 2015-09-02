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
using System.Windows;
using System.Windows.Threading;
using DustInTheWind.CoolApp.Wpf.Properties;

namespace DustInTheWind.CoolApp.Wpf.Utils
{
    internal class UserInterface
    {
        public Window MainWindow { get; set; }

        /// <summary>
        /// Displays the exception in a friendly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        public virtual void DisplayError(Exception ex)
        {
            DisplayError(ex.Message);
        }

        /// <summary>
        /// Displays the exception in a friendly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        /// <param name="message">The message text to be displayed along with the error message.</param>
        public void DisplayError(Exception ex, string message)
        {
            string text = string.IsNullOrEmpty(message)
                ? ex.Message
                : message + "\n" + ex.Message;

            DisplayError(text);
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public virtual void DisplayError(string message)
        {
            if (MainWindow == null)
                MessageBox.Show(message, ServicesResources.MessagesService_Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(MainWindow, message, ServicesResources.MessagesService_Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public virtual void DisplayInfo(string message)
        {
            if (MainWindow == null)
                MessageBox.Show(message, ServicesResources.MessagesService_Information_Title, MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(MainWindow, message, ServicesResources.MessagesService_Information_Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Displays a list of warnings to the user.
        /// </summary>
        /// <param name="warnings">The list of warnings to be displayed to the user.</param>
        public void DisplayWarnings(Exception[] warnings)
        {
            string message = BuildMessage(warnings);

            if (MainWindow == null)
                MessageBox.Show(message, ServicesResources.MessagesService_Warning_Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                MessageBox.Show(MainWindow, message, ServicesResources.MessagesService_Warning_Title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private static string BuildMessage(IEnumerable<Exception> warnings)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Exception warning in warnings)
                sb.AppendLine(warning.Message);

            return sb.ToString();
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

            MessageBoxResult dialogResult;

            dialogResult = MainWindow == null
                ? MessageBox.Show(text, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes)
                : MessageBox.Show(MainWindow, text, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

            return dialogResult == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Presents the user a warning and asks a question in a message box and returns a yes/no answer.
        /// </summary>
        /// <param name="text">The warning and question to be asked.</param>
        /// <param name="title">The text to be displayed in the title bar of the popup window.</param>
        /// <returns><c>true</c> if the user answered yes; <c>false</c> otherwise.</returns>
        public bool YesNoWarning(string text, string title = null)
        {
            if (title == null)
                title = ServicesResources.MessagesService_Warning_Title;

            MessageBoxResult dialogResult;

            dialogResult = MainWindow == null
                ? MessageBox.Show(text, title, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes)
                : MessageBox.Show(MainWindow, text, title, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);

            return dialogResult == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Asks the user a question in a message box and returns the answer as a nullable boolean.
        /// </summary>
        /// <param name="text">The question to be asked.</param>
        /// <param name="title">The text to be displayed in the title bar of the popup window.</param>
        /// <returns><c>true</c> if the user answered yes; <c>false</c> if the user answered no; <c>null</c> otherwise.</returns>
        public bool? YesNoCancelQuestion(string text, string title = null)
        {
            if (title == null)
                title = ServicesResources.MessagesService_Question_Title;

            MessageBoxResult dialogResult;

            dialogResult = MainWindow == null
                ? MessageBox.Show(text, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes)
                : MessageBox.Show(MainWindow, text, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

            switch (dialogResult)
            {
                default:
                    return null;

                case MessageBoxResult.No:
                    return false;

                case MessageBoxResult.Yes:
                    return true;
            }
        }

        public string RequestDirectory(string initialPath, string description)
        {
            return Environment.CurrentDirectory;
        }

        public void Dispatch(Action action)
        {
            Dispatcher.CurrentDispatcher.Invoke(action);
        }
    }
}