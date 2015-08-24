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

namespace DustInTheWind.CoolApp.Config
{
    /// <summary>
    /// Provides configuration values to be used in Azzul application.
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets the azzul configuration section from the configuration file.
        /// </summary>
        CoolConfigurationSection CoolConfig { get; }

        /// <summary>
        /// Event raised when some of the configuration values are changed.
        /// </summary>
        event EventHandler ConfigurationChanged;

        /// <summary>
        /// Saves the changed configuration values into the config file.
        /// </summary>
        void Save();

        /// <summary>
        /// Raises the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        void AnnounceConfigurationChanged();
    }
}