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

namespace DustInTheWind.CoolApp.Mvp
{
    /// <summary>
    /// Base class that contains base logic for a view.
    /// </summary>
    /// <typeparam name="TView">The type of the view managed by the presenter.</typeparam>
    public abstract class PresenterBase<TView> : IPresenter<TView>
        where TView : class, IView
    {
        /// <summary>
        /// The GUI object used to interact with the user.
        /// </summary>
        protected TView view;

        /// <summary>
        /// Gets or sets the view that represents the GUI.
        /// Once the view is set it cannot be changed.
        /// </summary>
        /// <exception cref="CoolException">Thrown if the view is already set.</exception>
        public TView View
        {
            get { return view; }
            set
            {
                if (view != null)
                    throw new ViewAlreadySetException();

                view = value;
                view.Presenter = this;

                OnViewAssigned(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the view that represents the GUI.
        /// Once the view is set it cannot be changed.
        /// </summary>
        /// <exception cref="CoolException">Thrown if the view is already set.</exception>
        IView IPresenter.View
        {
            get { return view; }
            set { View = value as TView; }
        }

        #region Event ViewAssigned

        /// <summary>
        /// Event raised when the view is assigned to the current instance.
        /// </summary>
        public event EventHandler ViewAssigned;

        /// <summary>
        /// Raises the <see cref="ViewAssigned"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnViewAssigned(EventArgs e)
        {
            if (ViewAssigned != null)
                ViewAssigned(this, e);
        }

        #endregion
    }
}