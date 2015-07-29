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

namespace ApplicationExit.WinForms.Common
{
    internal abstract class ButtonViewModelBase : ViewModelBase
    {
        private bool enabled;

        public abstract string Description { get; }

        public bool Enabled
        {
            get { return enabled; }
            protected set
            {
                enabled = value;
                OnPropertyChanged();
            }
        }

        protected ButtonViewModelBase()
        {
            enabled = true;
        }

        public void MouseEnter()
        {
            // to do: display a description into the status bar.
        }

        public void MouseLeave()
        {
            // to do: remove the description from the status bar.
        }

        public void Clicked()
        {
            if (!enabled)
                return;

            Execute();
        }

        protected abstract void Execute();
    }
}