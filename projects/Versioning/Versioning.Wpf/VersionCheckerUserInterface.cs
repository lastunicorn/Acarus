using System;
using System.Drawing;
using System.Windows;
using DustInTheWind.Versioning;
using DustInTheWind.Versioning.Check;
using DustInTheWind.Versioning.Config;
using DustInTheWind.Versioning.Download;
using Versioning.Wpf.Views;

namespace Versioning.Wpf
{
    class VersionCheckerUserInterface : IVersionCheckerUserInterface
    {
        private readonly VersionChecker versionChecker;
        private readonly FileDownloader fileDownloader;
        private readonly UserInterface userInterface;
        private readonly IVersionCheckerConfig versionCheckerConfig;

        private VersionCheckerWindow versionCheckerWindow;

        public string AppWebPage { get; set; }

        public Image Icon { get; set; }

        public VersionCheckerUserInterface(VersionChecker versionChecker, FileDownloader fileDownloader,
            UserInterface userInterface, IVersionCheckerConfig versionCheckerConfig)
        {
            if (versionChecker == null) throw new ArgumentNullException("versionChecker");
            if (fileDownloader == null) throw new ArgumentNullException("fileDownloader");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (versionCheckerConfig == null) throw new ArgumentNullException("versionCheckerConfig");

            this.versionChecker = versionChecker;
            this.fileDownloader = fileDownloader;
            this.userInterface = userInterface;
            this.versionCheckerConfig = versionCheckerConfig;
        }

        public void ShowVersionChecker(object owner)
        {
            if (versionCheckerWindow != null)
            {
                versionCheckerWindow.Activate();
            }
            else
            {
                versionCheckerWindow = new VersionCheckerWindow { Owner = owner as Window };
                versionCheckerWindow.Closed += (sender, args) => versionCheckerWindow = null;

                VersionCheckerViewModel viewModel = new VersionCheckerViewModel(versionChecker, fileDownloader, userInterface, versionCheckerConfig, this);
                viewModel.AppWebPage = AppWebPage;
                viewModel.Icon = Icon;

                versionCheckerWindow.DataContext = viewModel;

                versionCheckerWindow.Show();
            }
        }

        public void CloseVersionChecker()
        {
            if (versionCheckerWindow == null)
                return;

            versionCheckerWindow.Close();
        }
    }
}