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
using System.Windows.Input;

namespace ApplicationExit.Wpf.Main
{
    public class SaveCommand : ICommand
    {
        private readonly TheData theData;
        public event EventHandler CanExecuteChanged;

        public SaveCommand(TheData theData)
        {
            if (theData == null) throw new ArgumentNullException("theData");

            this.theData = theData;
            this.theData.Changed += HandleTheDataChanged;
        }

        private void HandleTheDataChanged(object sender, EventArgs eventArgs)
        {
            OnCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            return theData.IsModified;
        }

        public void Execute(object parameter)
        {
            theData.Save();
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}