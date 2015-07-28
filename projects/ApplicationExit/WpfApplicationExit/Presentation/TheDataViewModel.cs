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
using WpfApplicationExit.Business;
using WpfApplicationExit.Presentation.Common;

namespace WpfApplicationExit.Presentation
{
    public class TheDataViewModel : ViewModelBase
    {
        private readonly TheData theData;
        private bool isChanged;
        private Brush theColor;
        private string theText;

        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                isChanged = value;
                OnPropertyChanged();

                TheColor = isChanged ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
            }
        }

        public Brush TheColor
        {
            get { return theColor; }
            set
            {
                theColor = value;
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
            TheText = "alez";
        }

        private void HandleTheDataChanged(object sender, EventArgs eventArgs)
        {
            IsChanged = theData.IsModified;
        }
    }
}