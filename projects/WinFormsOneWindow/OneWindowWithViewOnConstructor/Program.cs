using System;
using System.Windows.Forms;

namespace DustInTheWind.OneWindowViewOnConstructor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            MainForm mainForm = new MainForm();
            MainPresenter mainPresenter = new MainPresenter(mainForm);
            mainForm.Presenter = mainPresenter;
            
            Application.Run(mainForm);
        }
    }
}
