using System.Windows.Forms;

namespace ApplicationExit.Presentation.UI
{
    partial class MainForm : Form
    {
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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !viewModel.FormIsClosing();
        }
    }
}