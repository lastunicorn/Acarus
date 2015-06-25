using System.Windows.Forms;

namespace DustInTheWind.OneWindowViewOnConstructor
{
    partial class MainForm : Form, IMainView
    {
        public MainPresenter Presenter { private get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        public string SourceText
        {
            get { return textBoxSource.Text; }
        }

        public string DestinationText
        {
            set { textBoxDestination.Text = value; }
        }

        private void HandleActionClicked(object sender, System.EventArgs e)
        {
            Presenter.ActionWasClicked();
        }
    }
}
