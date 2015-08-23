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
using DustInTheWind.Versioning;

namespace DustInTheWind.CoolApp.Versioning
{
    /// <summary>
    /// A service that checks if a newer version of Azzul exists.
    /// </summary>
    public interface IAzzulVersionChecker
    {
        /// <summary>
        /// The url of the version file.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets a value specifying if an asynchronous operation is in progress.
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Event raised when the version checker finishes an asynchronous check.
        /// </summary>
        event EventHandler<CheckCompletedEventArgs> CheckCompleted;

        /// <summary>
        /// Gets the result of the latest performed check.
        /// </summary>
        VersionCheckingResult LastCheckingResult { get; }

        /// <summary>
        /// Asynchronously compares the version obtained from the version provider with the current one.
        /// If another asynchronous check is in progress, nothing is done.
        /// </summary>
        /// <returns><c>true</c> if the asynchronous check was successfully started; <c>false</c> if another asynchronous check is already running.</returns>
        bool CheckAsync();
    }
}