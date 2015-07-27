using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationExit.Business;

namespace ApplicationExit.Presentation
{
    partial class MainForm : Form
    {
        private readonly MyApplication myApplication;

        private bool allowToExit;
        private MainViewModel viewModel;

        public MainForm()
        {
        }

        public MainForm(MyApplication myApplication)
        {
            InitializeComponent();

            if (myApplication == null) throw new ArgumentNullException("myApplication");

            this.myApplication = myApplication;

            viewModel = new MainViewModel(myApplication);

            Binding bind = new Binding("BackColor", viewModel, "IsDataChanged");
            bind.Format += (s, e) =>
            {
                e.Value = (bool)e.Value ? Color.LightSalmon : Color.LawnGreen;
            };
            labelData.DataBindings.Add(bind);

            customButtonExit.Model = new ExitButtonModel(myApplication);
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            myApplication.TheData.Save();
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
