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

using ApplicationExit.Wpf.Main;

namespace ApplicationExit.Wpf
{
    internal class Bootstrapper
    {
        public void Initialize()
        {
            UserInterface userInterface = new UserInterface();

            MyApplication myApplication = new MyApplication(userInterface);
            TheData theData = new TheData(userInterface, myApplication);

            MainWindow mainForm = new MainWindow
            {
                ViewModel = new MainViewModel(myApplication, theData)
            };

            userInterface.MainWindow = mainForm;
            userInterface.Run();
        }
    }
}