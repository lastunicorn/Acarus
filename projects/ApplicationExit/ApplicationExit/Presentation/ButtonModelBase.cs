using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ApplicationExit.Presentation
{
    internal abstract class ButtonModelBase : IButtonModel
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void MouseEnter();
        public abstract void MouseLeave();
        public abstract void Clicked();
    }
}