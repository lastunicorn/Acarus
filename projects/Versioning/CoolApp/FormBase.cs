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

using System.Windows.Forms;
using DustInTheWind.CoolApp.Mvp;
using DustInTheWind.CoolApp.Mvp.Window;

namespace DustInTheWind.CoolApp
{
    /// <summary>
    /// Base class for a Form in Azzul application.
    /// </summary>
    internal class FormBase<TPresenter> : Form, IWindowView<TPresenter>
        where TPresenter : class, IPresenter
    {
        /// <summary>
        /// Gets or sets the Presenter that contains the logic of the <see cref="Form"/>.
        /// </summary>
        public TPresenter Presenter { get; set; }

        /// <summary>
        /// Gets or sets the Presenter that contains the logic of the <see cref="Form"/>.
        /// </summary>
        IPresenter IView.Presenter
        {
            get { return Presenter; }
            set { Presenter = value as TPresenter; }
        }

        /// <summary>
        /// Sets the parent for the current form.
        /// The <see cref="parent"/> parameter is of type <see cref="IWindowView"/>, but for the procedure to success,
        /// it has to also inherits from <see cref="Form"/> class. If the <see cref="parent"/> does not inherits from <see cref="Form"/>
        /// the parent is set to null.
        /// </summary>
        /// <param name="parent">The parent view for the current form.</param>
        public void SetParent(IWindowView parent)
        {
            Owner = parent as Form;
        }

        /// <summary>
        /// Displays the view.
        /// </summary>
        public void ShowView()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(ShowView));
            }
            else
            {
                Show();
            }
        }

        /// <summary>
        /// Displays the view.
        /// </summary>
        public void ShowDialogView()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(ShowView));
            }
            else
            {
                ShowDialog();
            }
        }

        /// <summary>
        /// Focuses the view.
        /// </summary>
        public void FocusView()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(FocusView));
            }
            else
            {
                Focus();
            }
        }

        /// <summary>
        /// Closes the view.
        /// </summary>
        public void CloseView()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(CloseView));
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Hides the view.
        /// </summary>
        public void HideView()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(HideView));
            }
            else
            {
                Hide();
            }
        }

        /// <summary>
        /// Sets the result on "Ok" and closes the view.
        /// </summary>
        public void CloseViewOk()
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sets the result on "Cancel" and closes the view.
        /// </summary>
        public void CloseViewCancel()
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}