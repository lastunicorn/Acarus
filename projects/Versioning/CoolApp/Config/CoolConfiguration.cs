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
using DustInTheWind.CoolApp.Properties;

namespace DustInTheWind.CoolApp.Config
{
    /// <summary>
    /// Loads and stores the configuration values.
    /// </summary>
    public class CoolConfiguration : ICoolConfiguration
    {
        /// <summary>
        /// A <see cref="Configuration"/> objects that represents the configuration file managed by the current instance.
        /// </summary>
        private Configuration config;

        /// <summary>
        /// Gets the azzul configuration section from the configuration file.
        /// </summary>
        public CoolConfigurationSection CoolConfig { get; set; }

        #region ConfigurationSaved

        /// <summary>
        /// Event raised after the configuration values are written into the file.
        /// </summary>
        public event EventHandler ConfigurationSaved;

        /// <summary>
        /// Raises the <see cref="ConfigurationSaved"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnConfigurationSaved(EventArgs e)
        {
            if (ConfigurationSaved != null)
                ConfigurationSaved(this, e);
        }

        #endregion

        /// <summary>
        /// Reads the configuration file from the disk and initializes the current instance.
        /// </summary>
        public void Initialize()
        {
            config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            CoolConfig = GetOrCreateCoolSection();
        }

        /// <summary>
        /// Returns the azzul configuration section from the specified <see cref="Configuration"/> object.
        /// If no azzul section can be found, an empty one is created.
        /// </summary>
        /// <returns>A <see cref="CoolConfigurationSection"/> instance representing the azzul configuration section.</returns>
        /// <exception cref="CoolConfigurationException"></exception>
        private CoolConfigurationSection GetOrCreateCoolSection()
        {
            return GetCoolConfigurationSection() ?? CreateAndAddAzzulConfigurationSection();
        }

        private CoolConfigurationSection GetCoolConfigurationSection()
        {
            try
            {
                return config.GetSection(CoolConfigurationSection.DefaultSectionName) as CoolConfigurationSection;
            }
            catch (Exception ex)
            {
                string message = string.Format(Resources.Error_ConfigurationManager_ErrorReadingAzzulSection, ex.Message);
                throw new CoolConfigurationException(message, ex);
            }
        }

        private CoolConfigurationSection CreateAndAddAzzulConfigurationSection()
        {
            CoolConfigurationSection coolConfigurationSection = new CoolConfigurationSection();
            coolConfigurationSection.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
            coolConfigurationSection.SectionInformation.ForceSave = true;

            config.Sections.Add(CoolConfigurationSection.DefaultSectionName, coolConfigurationSection);

            return coolConfigurationSection;
        }

        /// <summary>
        /// Saves the changed configuration values into the config file.
        /// </summary>
        public void Save()
        {
            config.Save();
            OnConfigurationSaved(EventArgs.Empty);
        }
    }
}