using System;
using System.Windows.Forms;

namespace DustInTheWind.OneWindowViewProperty
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

            MainPresenter mainPresenter = new MainPresenter();
            mainPresenter.View = mainForm;

            mainForm.Presenter = mainPresenter;

            Application.Run(mainForm);
        }
    }
}
