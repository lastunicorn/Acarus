using System;
using System.Windows.Forms;
using ApplicationExit.Business;

namespace ApplicationExit.Presentation.UI
{
    partial class MainForm : Form
    {
        private readonly MyApplication myApplication;

        private bool allowToExit;

        public MainForm(MyApplication myApplication)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            this.myApplication = myApplication;

            InitializeComponent();

            MainViewModel viewModel = new MainViewModel(myApplication);

            theDataView.ViewModel = viewModel.TheDataModel;
            customButtonExit.ViewModel = viewModel.ExitButtonModel;
            buttonSave.ViewModel = viewModel.SaveButtonModel;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (allowToExit)
                return;

            allowToExit = true;
            bool allowToContinue = myApplication.Exit();

            if (!allowToContinue)
            {
                allowToExit = false;
                e.Cancel = true;
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            myApplication.TheData.ChangeData();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            allowToExit = true;

            bool allowToContinue = myApplication.Exit();

            if (!allowToContinue)
                allowToExit = false;
        }
    }
}
