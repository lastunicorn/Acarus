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

namespace DustInTheWind.Versioning.WinForms.Mvp.Common
{
    /// <summary>
    /// Displays messages to the user.
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Displays the exception in a friendly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        void DisplayError(Exception ex);

        /// <summary>
        /// Displays the exception in a friendly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        /// <param name="message">The message text to be displayed along with the error message.</param>
        void DisplayError(Exception ex, string message);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayErrorMessage(string message);

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayInfo(string message);

        /// <summary>
        /// Displays a list of warnings to the user.
        /// </summary>
        /// <param name="warnings">The list of warnings to be displayed to the user.</param>
        void DisplayWarnings(Exception[] warnings);

        /// <summary>
        /// Asks the user a question in a message box and returns a yes/no answer.
        /// </summary>
        /// <param name="text">The question to be asked.</param>
        /// <param name="title"></param>
        /// <returns><c>true</c> if the user answered yes; <c>false</c> otherwise.</returns>
        bool YesNoQuestion(string text, string title);

        /// <summary>
        /// Presents the user a warning and asks a question in a message box and returns a yes/no answer.
        /// </summary>
        /// <param name="text">The warning and question to be asked.</param>
        /// <returns><c>true</c> if the user answered yes; <c>false</c> otherwise.</returns>
        bool YesNoWarning(string text, string title = null);

        bool? YesNoCancelQuestion(string text, string title);

        /// <summary>
        /// Requests a directory path from the user.
        /// </summary>
        /// <param name="initialPath">The initial path displayed as a suggestion to the user.</param>
        /// <param name="description">The description text to explain what will the directory path used for.</param>
        /// <returns>The directory path selected by the user or null if the user canceled the action.</returns>
        string RequestDirectory(string initialPath, string description);

        void Dispatch(Action action);
    }
}