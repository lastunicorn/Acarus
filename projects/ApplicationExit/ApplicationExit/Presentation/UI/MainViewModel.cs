using System;
using ApplicationExit.Business;
using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    class MainViewModel : ViewModelBase
    {
        private readonly MyApplication myApplication;

        private bool allowToExit;

        public TheDataViewModel TheDataModel { get; set; }
        public ExitButtonModel ExitButtonModel { get; set; }
        public SaveButtonModel SaveButtonModel { get; set; }
        public ButtonViewModelBase ChangeButtonModel { get; set; }

        public MainViewModel(MyApplication myApplication)
        {
            this.myApplication = myApplication;
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            TheDataModel = new TheDataViewModel(myApplication.TheData);
            ExitButtonModel = new ExitButtonModel(myApplication);
            SaveButtonModel = new SaveButtonModel(myApplication.TheData);
            ChangeButtonModel = new ChangeButtonModel(myApplication.TheData);

            myApplication.BeforeExiting += HandleMyApplicationBeforeExiting;
            myApplication.AfterExiting += HandleMyApplicationAfterExiting;
        }

        private void HandleMyApplicationBeforeExiting(object sender, EventArgs eventArgs)
        {
            allowToExit = true;
        }

        private void HandleMyApplicationAfterExiting(object sender, EventArgs eventArgs)
        {
            allowToExit = false;
        }

        public bool FormIsClosing()
        {
            if (allowToExit)
                return true;

            return myApplication.Exit();
        }
    }
}