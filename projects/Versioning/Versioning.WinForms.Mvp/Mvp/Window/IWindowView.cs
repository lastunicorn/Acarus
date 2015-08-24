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

using System.ComponentModel;

namespace DustInTheWind.Versioning.WinForms.Mvp.Mvp.Window
{
    /// <summary>
    /// Represents a view of a window.
    /// </summary>
    public interface IWindowView : IView, ISynchronizeInvoke
    {
        /// <summary>
        /// Sets the parent for the current view.
        /// </summary>
        /// <param name="parent">The parent view for the current view.</param>
        void SetParent(IWindowView parent);

        /// <summary>
        /// Displays the view.
        /// </summary>
        void ShowView();

        /// <summary>
        /// Displays the view.
        /// </summary>
        void ShowDialogView();

        /// <summary>
        /// Focuses the view.
        /// </summary>
        void FocusView();

        /// <summary>
        /// Closes the view.
        /// </summary>
        void CloseView();

        /// <summary>
        /// Hides the view.
        /// </summary>
        void HideView();

        /// <summary>
        /// Sets the result on "Ok" and closes the view.
        /// </summary>
        void CloseViewOk();

        /// <summary>
        /// Sets the result on "Cancel" and closes the view.
        /// </summary>
        void CloseViewCancel();
    }
}