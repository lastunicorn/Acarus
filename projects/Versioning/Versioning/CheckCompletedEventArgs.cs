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
using System.ComponentModel;

namespace DustInTheWind.Versioning
{
    /// <summary>
    /// Provides data for <see cref="VersionChecker.CheckCompleted"/> event.
    /// </summary>
    public class CheckCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// The results of the version checking operation.
        /// </summary>
        private readonly VersionCheckingResult versionCheckingResult;

        /// <summary>
        /// Gets the results of the version checking operation.
        /// </summary>
        public VersionCheckingResult VersionCheckingResult
        {
            get
            {
                RaiseExceptionIfNecessary();
                return versionCheckingResult;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="versionCheckingResult">The results of the version checking operation.</param>
        public CheckCompletedEventArgs(VersionCheckingResult versionCheckingResult)
            : base(null, false, null)
        {
            this.versionCheckingResult = versionCheckingResult;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="error">The exception thrown by the version checker.</param>
        public CheckCompletedEventArgs(Exception error)
            : base(error, false, null)
        {
            versionCheckingResult = null;
        }
    }
}