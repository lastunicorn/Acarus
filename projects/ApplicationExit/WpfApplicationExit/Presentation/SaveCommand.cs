using System;
using System.Windows.Input;
using WpfApplicationExit.Business;

namespace WpfApplicationExit.Presentation
{
    public class SaveCommand : ICommand
    {
        private readonly TheData theData;
        public event EventHandler CanExecuteChanged;

        public SaveCommand(TheData theData)
        {
            if (theData == null) throw new ArgumentNullException("theData");

            this.theData = theData;
            this.theData.Changed += HandleTheDataChanged;
        }

        private void HandleTheDataChanged(object sender, EventArgs eventArgs)
        {
            OnCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            return theData.IsModified;
        }

        public void Execute(object parameter)
        {
            theData.Save();
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}