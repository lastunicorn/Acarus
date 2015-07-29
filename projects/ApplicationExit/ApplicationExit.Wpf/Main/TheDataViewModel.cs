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
using System.Windows.Media;
using ApplicationExit.Wpf.Common;

namespace ApplicationExit.Wpf.Main
{
    public class TheDataViewModel : ViewModelBase
    {
        private readonly TheData theData;
        private bool isChanged;
        private Brush theBackground;
        private string theText;
        private Brush theForeground;

        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                isChanged = value;
                OnPropertyChanged();

                TheBackground = isChanged ? new SolidColorBrush(Colors.IndianRed) : new SolidColorBrush(Colors.LawnGreen);
                TheForeground = isChanged ? new SolidColorBrush(Colors.Maroon) : new SolidColorBrush(Colors.Green);
                TheText = isChanged ? "Data is Changed" : "Data is Saved";
            }
        }

        public Brush TheBackground
        {
            get { return theBackground; }
            set
            {
                theBackground = value;
                OnPropertyChanged();
            }
        }

        public Brush TheForeground
        {
            get { return theForeground; }
            set
            {
                theForeground = value;
                OnPropertyChanged();
            }
        }

        public string TheText
        {
            get { return theText; }
            set
            {
                theText = value;
                OnPropertyChanged();
            }
        }

        public TheDataViewModel(TheData theData)
        {
            this.theData = theData;
            theData.Changed += HandleTheDataChanged;

            IsChanged = theData.IsModified;
        }

        private void HandleTheDataChanged(object sender, EventArgs eventArgs)
        {
            IsChanged = theData.IsModified;
        }
    }
}