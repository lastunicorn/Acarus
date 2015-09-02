// Acarus
// Copyright (C) 2015 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Configuration;
using DustInTheWind.Versioning.Download;

namespace DustInTheWind.Versioning.Wpf
{
    public class VersioningModule : VersioningModuleBase
    {
        public VersioningModule(Configuration config)
            : base(config)
        {
        }

        protected override IUserInterface CreateUserInterfaceHelper()
        {
            return new UserInterface();
        }

        protected override IVersionCheckerUserInterface CreateVersionCheckerUserInterface(IUserInterface userInterface)
        {
            FileDownloader fileDownloader = new FileDownloader(userInterface);

            return new VersionCheckerUserInterface(Checker, fileDownloader, userInterface, Config);
        }
    }
}