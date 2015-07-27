using System.ComponentModel;

namespace ApplicationExit.Presentation
{
    internal interface IButtonModel : INotifyPropertyChanged
    {
        void MouseEnter();
        void MouseLeave();
        void Clicked();
    }
}