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

namespace DustInTheWind.CoolApp.Config
{
    /// <summary>
    /// Represents a configuration element containing values that specify how to perform the update.
    /// </summary>
    public class UpdateConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the url of the file containing the update information.
        /// This is used only in debug for testing the update mechanism.
        /// </summary>
        [ConfigurationProperty("url", DefaultValue = "", IsRequired = false)]
        public string Url
        {
            get { return (string) this["url"]; }
            set { this["url"] = value; }
        }

        /// <summary>
        /// Gets or sets a value that specifies if an update check should be performed at the application's start up.
        /// Default value: true.
        /// </summary>
        [ConfigurationProperty("checkAtStartup", DefaultValue = true, IsRequired = false)]
        public bool CheckAtStartup
        {
            get { return (bool) this["checkAtStartup"]; }
            set { this["checkAtStartup"] = value; }
        }
    }
}