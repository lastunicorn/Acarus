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

using System.Windows;

namespace WpfApplicationExit.Business
{
    public class UserInterface
    {
        public Window MainWindow { get; set; }

        public void Run()
        {
            MainWindow.Show();
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void DisplayInfo(string text)
        {
            MessageBox.Show(MainWindow, text, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool? DisplayYesNoQuestion(string question, string title)
        {
            MessageBoxResult dialogResult = MessageBox.Show(MainWindow, question, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Cancel)
                return null;

            return dialogResult == MessageBoxResult.Yes;
        }
    }
}