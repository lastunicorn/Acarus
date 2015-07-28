using System;
using System.Windows.Forms;
using ApplicationExit.Presentation;

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
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Initialize();
        }
    }
}