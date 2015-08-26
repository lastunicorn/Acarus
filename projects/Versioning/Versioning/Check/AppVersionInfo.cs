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

namespace DustInTheWind.Versioning.Check
{
    /// <summary>
    /// Contains information about an application version like the version number, a short description, etc.
    /// </summary>
    public class AppVersionInfo
    {
        /// <summary>
        /// Gets the version value.
        /// </summary>
        public Version Version { get; private set; }

        /// <summary>
        /// Gets the name of the application for which the version applies.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a string containing the version.
        /// </summary>
        public string InformationalVersion { get; private set; }

        /// <summary>
        /// Gets a short description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the url path from where the application can be downloaded.
        /// </summary>
        public string DownloadUrl { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppVersionInfo"/> class.
        /// </summary>
        /// <param name="version">The version object.</param>
        /// <exception cref="ArgumentNullException">The version parameter is null.</exception>
        public AppVersionInfo(Version version)
            : this(null, version, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppVersionInfo"/> class.
        /// </summary>
        /// <param name="name">The name of the application.</param>
        /// <param name="version">The version of the application.</param>
        /// <param name="informationalVersion">The string representation of the version. It may contain additional information other then digits.</param>
        /// <param name="description">A short description of the version.</param>
        /// <param name="downloadUrl">The url path from where the application can be downloaded.</param>
        /// <exception cref="ArgumentNullException">The version argument is null.</exception>
        public AppVersionInfo(string name, Version version, string informationalVersion, string description, string downloadUrl)
        {
            if (version == null)
                throw new ArgumentNullException("version");

            Name = name;
            Version = version;
            InformationalVersion = informationalVersion;
            Description = description;
            DownloadUrl = downloadUrl;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as AppVersionInfo);
        }

        public bool Equals(AppVersionInfo ver)
        {
            if (ver == null)
                return false;

            return Name == ver.Name &&
                   Version == ver.Version &&
                   InformationalVersion == ver.InformationalVersion &&
                   Description == ver.Description &&
                   DownloadUrl == ver.DownloadUrl;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            string[] tmp =
            {
                Name,
                Version.ToString(),
                InformationalVersion,
                Description,
                DownloadUrl
            };

            return string.Join("", tmp).GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string representation of the current instance.</returns>
        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Version, InformationalVersion, Description);
        }
    }
}