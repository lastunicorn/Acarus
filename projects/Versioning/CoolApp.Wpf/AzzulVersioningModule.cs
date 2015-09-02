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

using System;
using System.Configuration;
using DustInTheWind.Versioning.Wpf;

namespace DustInTheWind.CoolApp.Wpf
{
    internal class AzzulVersioningModule : VersioningModule
    {
        public AzzulVersioningModule(Configuration config)
            : base(config)
        {
            var url = Config.Url;

            if (string.IsNullOrEmpty(url))
                Config.Url = "http://azzul.alez.ro/appinfo.xml";

            Checker.AppName = "Azzul";
            Checker.CurrentVersion = new Version(1, 2, 3, 4);
        }
    }
}