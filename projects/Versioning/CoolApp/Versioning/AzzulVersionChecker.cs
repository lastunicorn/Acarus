// Azzul
// Copyright (C) 2009-2011 Dust in the Wind
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
using DustInTheWind.CoolApp.Config;
using DustInTheWind.CoolApp.Services;
using DustInTheWind.Versioning;

namespace DustInTheWind.CoolApp.Versioning
{
    /// <summary>
    /// A service that checks if a newer version of Azzul exists on the internet.
    /// </summary>
    public class AzzulVersionChecker : VersionChecker, IAzzulVersionChecker
    {
        /// <summary>
        /// The default version file url. It is used only if the configuration object does not provide an url. 
        /// </summary>
        public const string DefaultCheckUrl = "http://azzul.alez.ro/appinfo.xml";

        /// <summary>
        /// The url of the version file.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzzulVersionChecker"/> class.
        /// </summary>
        /// <param name="azzulConfiguration"> Provides configuration values to be used in Azzul application.</param>
        /// <param name="applicationService">Provides different information about the Azzul application.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if one of the arguments is null.</exception>
        public AzzulVersionChecker(IAzzulConfiguration azzulConfiguration, IApplicationService applicationService)
        {
            if (azzulConfiguration == null) throw new ArgumentNullException("azzulConfiguration");
            if (applicationService == null) throw new ArgumentNullException("applicationService");

            MinCheckTime = TimeSpan.FromSeconds(1);
            CurrentVersion = new Version(1, 4, 0, 35439);

            Url = GetRepositoryUrl(azzulConfiguration);

            AppInfoProvider = new HttpAppVersionInfoProvider
            {
                Url = Url,
                AppName = "Azzul"
            };
        }

        private static string GetRepositoryUrl(IAzzulConfiguration azzulConfiguration)
        {
            bool existsCustomUrl = azzulConfiguration.AzzulConfig != null && !string.IsNullOrEmpty(azzulConfiguration.AzzulConfig.Update.Url);
            return existsCustomUrl ? azzulConfiguration.AzzulConfig.Update.Url : DefaultCheckUrl;
        }
    }
}
