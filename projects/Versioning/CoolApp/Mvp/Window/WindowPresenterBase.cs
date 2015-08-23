// Azzul
// Copyright (C) 2009-2011 Dust in the Wind
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

namespace DustInTheWind.CoolApp.Mvp.Window
{
    /// <summary>
    /// Base class that contains base logic for a view of a window.
    /// </summary>
    /// <typeparam name="TView">The type of the view managed by the presenter.</typeparam>
    public abstract class WindowPresenterBase<TView> : PresenterBase<TView>, IWindowPresenter<TView>
        where TView : class, IWindowView
    {
        /// <summary>
        /// A service that displays messages to the user.
        /// </summary>
        protected readonly IMessagesService messagesService;

        #region Event ViewClosed

        /// <summary>
        /// Event raised after the view is closed.
        /// </summary>
        public event EventHandler ViewClosed;

        /// <summary>
        /// Raises the <see cref="ViewWasClosed"/> event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnViewClosed(EventArgs e)
        {
            if (ViewClosed != null)
                ViewClosed(this, e);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowPresenterBase{T}"/> class.
        /// </summary>
        /// <param name="messagesService">A service that displays messages to the user.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if the messagesService argument is null.</exception>
        protected WindowPresenterBase(IMessagesService messagesService)
        {
            if (messagesService == null)
                throw new ArgumentNullException("messagesService");

            this.messagesService = messagesService;
        }

        /// <summary>
        /// TestMe called by the view after the view was closed.
        /// </summary>
        public virtual void ViewWasClosed()
        {
            try
            {
                OnViewClosed(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                messagesService.DisplayError(ex);
            }
        }

        /// <summary>
        /// Displays the view.
        /// </summary>
        public virtual void ShowView()
        {
            try
            {
                view.ShowView();
            }
            catch (Exception ex)
            {
                messagesService.DisplayError(ex);
            }
        }

        /// <summary>
        /// Puts the focus on the view.
        /// </summary>
        public virtual void Focus()
        {
            try
            {
                view.FocusView();
            }
            catch (Exception ex)
            {
                messagesService.DisplayError(ex);
            }
        }
    }
}
