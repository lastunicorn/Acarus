using System.ComponentModel;

namespace ApplicationExit.Wpf.Presentation
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MainViewModel mainViewModel = DataContext as MainViewModel;

            if (mainViewModel != null)
                e.Cancel = !mainViewModel.WindowIsClosing();
        }
    }
}