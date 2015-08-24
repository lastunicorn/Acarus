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
    /// Provides access to the configuration values of the azzul application.
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly ICoolConfiguration coolConfiguration;

        /// <summary>
        /// Gets the azzul configuration section from the configuration file.
        /// </summary>
        public CoolConfigurationSection CoolConfig
        {
            set { coolConfiguration.CoolConfig = value; }
            get { return coolConfiguration.CoolConfig; }
        }

        #region RecentCatalogAdded

        /// <summary>
        /// Event raised when a catalog is added to the list of recent catalogs.
        /// </summary>
        public event EventHandler RecentCatalogAdded;

        /// <summary>
        /// Raises the <see cref="RecentCatalogAdded"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnRecentCatalogAdded(EventArgs e)
        {
            if (RecentCatalogAdded != null)
                RecentCatalogAdded(this, e);
        }

        #endregion

        #region ConfigurationChanged

        /// <summary>
        /// Event raised when some of the configuration values are changed.
        /// </summary>
        public event EventHandler ConfigurationChanged;

        /// <summary>
        /// Raises the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnConfigurationChanged(EventArgs e)
        {
            if (ConfigurationChanged != null)
                ConfigurationChanged(this, e);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        /// <param name="coolConfiguration"></param>
        public ConfigurationManager(ICoolConfiguration coolConfiguration)
        {
            if (coolConfiguration == null)
                throw new ArgumentNullException("coolConfiguration");

            this.coolConfiguration = coolConfiguration;
        }

        /// <summary>
        /// Saves the changed configuration values into the config file.
        /// </summary>
        public void Save()
        {
            coolConfiguration.Save();
        }

        /// <summary>
        /// Raises the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        public void AnnounceConfigurationChanged()
        {
            OnConfigurationChanged(EventArgs.Empty);
        }
    }
}