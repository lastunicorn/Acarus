using System;
using WpfApplicationExit.Business;
using WpfApplicationExit.Presentation.Common;

namespace WpfApplicationExit.Presentation
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MyApplication myApplication;
        private bool allowToExit;

        public TheDataViewModel TheDataViewModel { get; set; }
        public SaveCommand SaveCommand { get; set; }
        public ChangeCommand ChangeCommand { get; set; }
        public ExitCommand ExitCommand { get; set; }

        public MainViewModel(MyApplication myApplication, TheData theData)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");
            if (theData == null) throw new ArgumentNullException("theData");

            this.myApplication = myApplication;

            TheDataViewModel = new TheDataViewModel(theData);
            SaveCommand = new SaveCommand(theData);
            ChangeCommand = new ChangeCommand(theData);
            ExitCommand = new ExitCommand(myApplication);

            myApplication.BeforeExiting += HandleMyApplicationBeforeExiting;
            myApplication.ExitCanceled += HandleMyApplicationExitCanceled;
        }

        private void HandleMyApplicationBeforeExiting(object sender, EventArgs eventArgs)
        {
            allowToExit = true;
        }

        private void HandleMyApplicationExitCanceled(object sender, EventArgs eventArgs)
        {
            allowToExit = false;
        }

        public bool WindowIsClosing()
        {
            return allowToExit || myApplication.Exit();
        }
    }
}