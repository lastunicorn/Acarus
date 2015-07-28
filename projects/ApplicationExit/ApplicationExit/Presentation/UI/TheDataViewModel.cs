using System;
using ApplicationExit.Business;
using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    class TheDataViewModel : ViewModelBase
    {
        private readonly TheData theData;
        private bool isChanged;

        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                isChanged = value;
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