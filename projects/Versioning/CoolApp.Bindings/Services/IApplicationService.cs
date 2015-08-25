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

namespace DustInTheWind.CoolApp.Services
{
    /// <summary>
    /// Provides different information about the Azzul application.
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Returns the version of the application.
        /// </summary>
        /// <returns>A <see cref="Version"/> object containing the current version of Azzul.</returns>
        Version GetCurrentVersion();

        /// <summary>
        /// Returns the name of the application.
        /// </summary>
        /// <returns>The name of the application.</returns>
        string GetApplicationName();

        /// <summary>
        /// Returns the path where the application's executable file is located.
        /// </summary>
        /// <returns>The path where the application's executable file is located.</returns>
        string GetApplicationLocation();
    }
}