using System;
using ApplicationExit.Business;
using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    class MainViewModel : ViewModelBase
    {
        private readonly MyApplication myApplication;

        private bool allowToExit;

        public TheDataViewModel TheDataModel { get; private set; }
        public ExitButtonModel ExitButtonModel { get; private set; }
        public SaveButtonModel SaveButtonModel { get; private set; }
        public ChangeButtonModel ChangeButtonModel { get; private set; }

        public MainViewModel(MyApplication myApplication, TheData theData)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");
            if (theData == null) throw new ArgumentNullException("theData");

            this.myApplication = myApplication;

            TheDataModel = new TheDataViewModel(theData);
            ExitButtonModel = new ExitButtonModel(myApplication);
            SaveButtonModel = new SaveButtonModel(theData);
            ChangeButtonModel = new ChangeButtonModel(theData);

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
            return allowToExit || myApplication.Exit();
        }
    }
}