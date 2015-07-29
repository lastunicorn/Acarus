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
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApplicationExit.Wpf.Main
{
    public class ExitCommand : ICommand
    {
        private readonly MyApplication myApplication;
        public event EventHandler CanExecuteChanged;

        public ExitCommand(MyApplication myApplication)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            this.myApplication = myApplication;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExitImmediately();
            //ExitWithDelay(1000);
        }

        private void ExitImmediately()
        {
            myApplication.Exit();
        }

        private void ExitWithDelay(int millisecondsDelay)
        {
            Task.Delay(millisecondsDelay)
                .ContinueWith(t => myApplication.Exit());
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}