using System;
using System.Windows.Forms;
using ApplicationExit.Business;

namespace ApplicationExit.Presentation.UI
{
    partial class MainForm : Form
    {
        private readonly MyApplication myApplication;

        private bool allowToExit;
        private MainViewModel viewModel;

        public MainViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != null)
                {
                    theDataView.ViewModel = null;
                    customButtonExit.ViewModel = null;
                    buttonSave.ViewModel = null;
                    buttonChange.ViewModel = null;
                }

                viewModel = value;

                if (viewModel != null)
                {
                    theDataView.ViewModel = viewModel.TheDataModel;
                    customButtonExit.ViewModel = viewModel.ExitButtonModel;
                    buttonSave.ViewModel = viewModel.SaveButtonModel;
                    buttonChange.ViewModel = viewModel.ChangeButtonModel;
                }
            }
        }

        public MainForm(MyApplication myApplication)
        {
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            this.myApplication = myApplication;

            InitializeComponent();

            //myApplication.BeforeExiting += HandleMyApplicationBeforeExiting;
        }

        //private void HandleMyApplicationBeforeExiting(object sender, EventArgs eventArgs)
        //{
        //    allowToExit = true;
        //}

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

        private void buttonExit_Click(object sender, EventArgs e)
        {
            allowToExit = true;

            bool allowToContinue = myApplication.Exit();

            if (!allowToContinue)
                allowToExit = false;
        }
    }
}
