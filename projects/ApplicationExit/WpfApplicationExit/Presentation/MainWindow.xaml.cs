namespace WpfApplicationExit.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainViewModel ViewModel
        {
            set
            {
                DataContext = value;

                if (DataContext != null)
                {
                    TheDataView.DataContext = value.TheDataViewModel;
                }
            }
        }
    }
}
