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
    /// Declares the methods for a class that provides version information about the application.
    /// </summary>
    public interface IAppVersionInfoProvider
    {
        string Location { get; }

        /// <summary>
        /// When implemented in a derived class, returns a <see cref="AppVersionInfo"/> object containing information
        /// about the newest version of the application.
        /// </summary>
        /// <returns>A <see cref="AppVersionInfo"/>Object containing information about the version.</returns>
        /// <exception cref="ArgumentNullException">The application name to be checked is null.</exception>
        /// <exception cref="VersionCheckingException">Some error</exception>
        /// <exception cref="VersionDocumentRetrieveException">The document containing the version information could not be retrieved.</exception>
        AppVersionInfo GetVersionInformation();
    }
}