using System;
using System.Windows.Input;
using ApplicationExit.Wpf.Business;

namespace ApplicationExit.Wpf.Presentation
{
    public class ChangeCommand : ICommand
    {
        private readonly TheData theData;
        public event EventHandler CanExecuteChanged;

        public ChangeCommand(TheData theData)
        {
            if (theData == null) throw new ArgumentNullException("theData");

            this.theData = theData;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            theData.ChangeData();
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}