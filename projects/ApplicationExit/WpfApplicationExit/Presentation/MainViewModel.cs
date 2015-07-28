using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using WpfApplicationExit.Business;
using WpfApplicationExit.Presentation.Common;

namespace WpfApplicationExit.Presentation
{
    public class MainViewModel : ViewModelBase
    {
        public TheDataViewModel TheDataViewModel { get; set; }

        public SaveCommand SaveCommand { get; set; }
        public ChangeCommand ChangeCommand { get; set; }

        public MainViewModel(MyApplication myApplication, TheData theData)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");
            if (theData == null) throw new ArgumentNullException("theData");

            TheDataViewModel = new TheDataViewModel(theData);
            SaveCommand = new SaveCommand(theData);
            ChangeCommand = new ChangeCommand(theData);
        }
    }
}