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

using DustInTheWind.Versioning.WinForms.Mvp.Mvp.Window;

namespace DustInTheWind.Versioning.WinForms.Mvp.Versioning
{
    /// <summary>
    /// Declares the members that should be implemented by a GUI object used by <see cref="DiskCreatorPresenter"/> as a view to interact with the user.
    /// </summary>
    public interface IVersionCheckerView : IWindowView<VersionCheckerPresenter>
    {
        /// <summary>
        /// Sets the visibility value of the progress bar.
        /// </summary>
        bool ProgressBarVisible { set; }

        /// <summary>
        /// Sets the mode in which the progress bar will work:
        /// Percent (will display a percentage);
        /// Loading (will display an infinite loading bar).
        /// </summary>
        ProgressBarType ProgressBarType { set; }

        /// <summary>
        /// If the progress bar is in Percent mode, sets the percentage value displayed.
        /// </summary>
        int ProgressBarValue { set; }

        /// <summary>
        /// Gets or sets the checked value of the "Check at Startup" check box.
        /// </summary>
        bool CheckAtStartupValue { get; set; }

        /// <summary>
        /// Enables or desables the "Check at Startup" check box.
        /// </summary>
        bool CheckAtStartupEnabled { set; }

        /// <summary>
        /// Sets the visibility value of the "Download" button.
        /// </summary>
        bool DownloadButtonVisible { set; }

        /// <summary>
        /// Sets the visibility value of the "Open Downloaded File" button.
        /// </summary>
        bool OpenDownloadedFileButtonVisible { set; }

        /// <summary>
        /// Sets the the "Check Again" button's enable value.
        /// </summary>
        bool CheckAgainButtonEnabled { set; }

        /// <summary>
        /// Sets the status text displayed in the header of the view.
        /// </summary>
        string StatusText { set; }

        /// <summary>
        /// Sets the information text displayed in the body of the view.
        /// </summary>
        string InformationText { set; }

        /// <summary>
        /// Requests a directory path from the user.
        /// </summary>
        /// <param name="initialPath">The initial path displayed as a suggestion to the user.</param>
        /// <param name="description">The description text to explain what will the directory path used for.</param>
        /// <returns>The directory path selected by the user or null if the user canceled the action.</returns>
        string RequestDirectory(string initialPath, string description);

        /// <summary>
        /// Asks the user if he allows to overwrite the specified file.
        /// </summary>
        /// <param name="message">The question text to be displayed to the user..</param>
        /// <returns><c>true</c> if the user allows the file to be overwritten; <c>false</c> otherise.</returns>
        bool AskOverwriteFile(string message);
    }
}