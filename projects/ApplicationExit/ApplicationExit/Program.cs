using System;
using System.Windows.Forms;
using ApplicationExit.Business;
using ApplicationExit.Presentation;
using ApplicationExit.Presentation.UI;

namespace ApplicationExit
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Initialize();
        }
    }

    internal class Bootstrapper
    {
        public void Initialize()
        {
            UserInterface userInterface = new UserInterface();
            TheData theData = new TheData(userInterface);

            MyApplication myApplication = new MyApplication(userInterface, theData);

            MainForm mainForm = new MainForm(myApplication)
            {
                ViewModel = new MainViewModel(myApplication)
            };

            userInterface.MainForm = mainForm;
            userInterface.Run();
        }
    }
}