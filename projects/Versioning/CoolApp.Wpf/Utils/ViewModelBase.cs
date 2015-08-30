using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DustInTheWind.CoolApp.Wpf
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public bool IsInitializing { get; private set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Initialize(Action action)
        {
            IsInitializing = true;

            try
            {
                action();
            }
            finally
            {
                IsInitializing = false;
            }
        }
    }
}