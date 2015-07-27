using System;
using ApplicationExit.Business;

namespace ApplicationExit.Presentation
{
    class ExitButtonModel : ButtonModelBase
    {
        private readonly MyApplication myApplication;
        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                OnPropertyChanged();
            }
        }

        public ExitButtonModel(MyApplication myApplication)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            this.myApplication = myApplication;
            enabled = true;
        }

        public override void MouseEnter()
        {

        }

        public override void MouseLeave()
        {

        }

        public override void Clicked()
        {
            myApplication.Exit();
        }
    }
}
