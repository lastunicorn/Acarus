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

namespace DustInTheWind.Versioning.WinForms.Mvp.Config
{
    /// <summary>
    /// Loads and stores the configuration values.
    /// </summary>
    public interface ICoolConfiguration
    {
        /// <summary>
        /// Gets the azzul configuration section from the configuration file.
        /// </summary>
        CoolConfigurationSection CoolConfig { get; set; }

        /// <summary>
        /// Event raised when the configuration values are written into the persistent location. (usually a file on disk)
        /// </summary>
        event EventHandler ConfigurationSaved;

        /// <summary>
        /// Reads the configuration file from the disk and initializes the current instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Saves the changed configuration values into the persistent location.
        /// </summary>
        void Save();
    }
}