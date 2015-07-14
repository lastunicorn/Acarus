using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleBinding
{
    // (1) implement INotifyPropertyChanged in the model.
    internal class SomeData : INotifyPropertyChanged
    {
        private string someText;

        public string SomeText
        {
            get { return someText; }
            set
            {
                someText = value;

                // (1') raise the PropertyChanged event when the value of the property changes.
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}