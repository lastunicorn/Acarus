using System;
using ApplicationExit.Business;
using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    class ExitButtonModel : ButtonViewModelBase
    {
        private readonly MyApplication myApplication;

        public override string Description
        {
            get { return "Exits the application"; }
        }

        public ExitButtonModel(MyApplication myApplication)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            this.myApplication = myApplication;
        }

        protected override void Execute()
        {
            myApplication.Exit();
        }
    }
}
