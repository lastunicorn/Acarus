using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplicationExit.Business;

namespace WpfApplicationExit.Presentation
{
    public class ExitCommand : ICommand
    {
        private readonly MyApplication myApplication;
        public event EventHandler CanExecuteChanged;

        public ExitCommand(MyApplication myApplication)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            this.myApplication = myApplication;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Task.Delay(1000)
                .ContinueWith(t => myApplication.Exit());
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}