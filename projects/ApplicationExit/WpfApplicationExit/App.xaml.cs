﻿using System.Windows;

namespace WpfApplicationExit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Initialize();

            base.OnStartup(e);
        }
    }
}