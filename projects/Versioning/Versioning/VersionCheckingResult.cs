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

namespace DustInTheWind.Versioning
{
    /// <summary>
    /// Contains the results of a version checking operation.
    /// </summary>
    public class VersionCheckingResult
    {
        /// <summary>
        /// Gets or sets the reference version. Usually this is the current version of the application.
        /// </summary>
        public Version CurrentVersion { get; set; }

        /// <summary>
        /// Gets or sets the version information retrieved from another place. Usually from the internet.
        /// </summary>
        public AppVersionInfo RetrievedAppVersionInfo { get; set; }

        /// <summary>
        /// Gets or sets the result of the version comparation.
        /// Less then 0 if the version obtained from the provider is older; 0 if the versions are equal; greater then 0 if the version obtained from the provider is newer.
        /// </summary>
        public int ComparationResult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the retrieved version is newer then the reference one.
        /// </summary>
        public bool IsNewerVersion { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            VersionCheckingResult versionCheckingResult = obj as VersionCheckingResult;

            if (versionCheckingResult == null)
                return false;

            return CurrentVersion ==
                   versionCheckingResult.CurrentVersion &&
                   (
                       (RetrievedAppVersionInfo == null && versionCheckingResult.RetrievedAppVersionInfo == null) ||
                       RetrievedAppVersionInfo != null && RetrievedAppVersionInfo.Equals(versionCheckingResult.RetrievedAppVersionInfo)
                       ) &&
                   ComparationResult == versionCheckingResult.ComparationResult &&
                   IsNewerVersion == versionCheckingResult.IsNewerVersion;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            int hashCode = 0;

            if (CurrentVersion != null)
                hashCode ^= CurrentVersion.GetHashCode();

            if (RetrievedAppVersionInfo != null)
                hashCode ^= RetrievedAppVersionInfo.GetHashCode();

            hashCode ^= ComparationResult.GetHashCode();

            hashCode ^= IsNewerVersion.GetHashCode();

            return hashCode;
        }
    }
}