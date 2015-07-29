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
using ApplicationExit.Wpf.Common;

namespace ApplicationExit.Wpf.Main
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MyApplication myApplication;
        private bool allowToExit;

        public TheDataViewModel TheDataViewModel { get; set; }
        public SaveCommand SaveCommand { get; set; }
        public ChangeCommand ChangeCommand { get; set; }
        public ExitCommand ExitCommand { get; set; }

        public MainViewModel(MyApplication myApplication, TheData theData)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");
            if (theData == null) throw new ArgumentNullException("theData");

            this.myApplication = myApplication;

            TheDataViewModel = new TheDataViewModel(theData);
            SaveCommand = new SaveCommand(theData);
            ChangeCommand = new ChangeCommand(theData);
            ExitCommand = new ExitCommand(myApplication);

            myApplication.BeforeExiting += HandleMyApplicationBeforeExiting;
            myApplication.ExitCanceled += HandleMyApplicationExitCanceled;
        }

        private void HandleMyApplicationBeforeExiting(object sender, EventArgs eventArgs)
        {
            allowToExit = true;
        }

        private void HandleMyApplicationExitCanceled(object sender, EventArgs eventArgs)
        {
            allowToExit = false;
        }

        public bool WindowIsClosing()
        {
            return allowToExit || myApplication.Exit();
        }
    }
}