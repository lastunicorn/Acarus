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

namespace DustInTheWind.Versioning
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
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayError(string message);

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayInfo(string message);

        /// <summary>
        /// Asks the user a question in a message box and returns a yes/no answer.
        /// </summary>
        /// <param name="text">The question to be asked.</param>
        /// <param name="title">The text to be displayed in the title bar of the popup window.</param>
        /// <returns><c>true</c> if the user answered yes; <c>false</c> otherwise.</returns>
        bool YesNoQuestion(string text, string title = null);

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