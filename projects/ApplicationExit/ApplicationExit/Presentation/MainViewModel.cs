using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ApplicationExit.Business;

namespace ApplicationExit.Presentation
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly MyApplication myApplication;
        private bool isDataChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsDataChanged
        {
            get { return isDataChanged; }
            set
            {
                isDataChanged = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(MyApplication myApplication)
        {
            this.myApplication = myApplication;
            myApplication.TheData.Changed += HandleTheDataChanged;

            IsDataChanged = myApplication.TheData.IsModified;
        }

        private void HandleTheDataChanged(object sender, EventArgs eventArgs)
        {
            IsDataChanged = myApplication.TheData.IsModified;
        }
    }
}