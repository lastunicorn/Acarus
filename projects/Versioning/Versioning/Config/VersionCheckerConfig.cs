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

namespace DustInTheWind.Versioning.Config
{
    public class VersionCheckerConfig : IVersionCheckerConfig
    {
        private readonly Configuration config;

        private VersionCheckerConfigurationSection ConfigSection
        {
            get
            {
                VersionCheckerConfigurationSection configSection = (VersionCheckerConfigurationSection)config.GetSection(VersionCheckerConfigurationSection.DefaultSectionName);

                if (configSection == null)
                {
                    configSection = new VersionCheckerConfigurationSection();
                    config.Sections.Add(VersionCheckerConfigurationSection.DefaultSectionName, configSection);
                }

                return configSection;
            }
        }

        public bool CheckAtStartUp
        {
            get { return ConfigSection.CheckAtStartup; }
            set
            {
                VersionCheckerConfigurationSection configSection = ConfigSection;

                if (configSection.CheckAtStartup == value)
                    return;

                configSection.CheckAtStartup = value;
                OnCheckAtStartupChanged();
            }
        }

        public string Url
        {
            get { return ConfigSection.Url; }
            set
            {
                VersionCheckerConfigurationSection configSection = ConfigSection;

                if (configSection.Url == value)
                    return;

                ConfigSection.Url = value;
                OnUrlChanged();
            }
        }

        public event EventHandler CheckAtStartUpChanged;
        public event EventHandler UrlChanged;

        public VersionCheckerConfig(Configuration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            this.config = config;
        }

        protected virtual void OnCheckAtStartupChanged()
        {
            EventHandler handler = CheckAtStartUpChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnUrlChanged()
        {
            EventHandler handler = UrlChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
