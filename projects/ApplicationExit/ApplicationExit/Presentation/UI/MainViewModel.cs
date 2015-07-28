using System;
using ApplicationExit.Business;
using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    class MainViewModel : ViewModelBase
    {
        public TheDataViewModel TheDataModel { get; set; }
        public ExitButtonModel ExitButtonModel { get; set; }
        public SaveButtonModel SaveButtonModel { get; set; }

        public MainViewModel(MyApplication myApplication)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            TheDataModel = new TheDataViewModel(myApplication.TheData);
            ExitButtonModel = new ExitButtonModel(myApplication);
            SaveButtonModel = new SaveButtonModel(myApplication.TheData);
        }
    }
}