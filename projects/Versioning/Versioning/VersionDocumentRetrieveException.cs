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
using System.Runtime.Serialization;
using DustInTheWind.Versioning.Properties;

namespace DustInTheWind.Versioning
{
    /// <summary>
    /// General exception raised by the DustInTheWind util classes.
    /// </summary>
    [Serializable]
    public class VersionDocumentRetrieveException : VersionCheckingException
    {
        private static readonly string DefaultMessage = VersioningResources.Error_VersionDocumentCannotBeRetrieved;

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionDocumentRetrieveException"/> class.
        /// </summary>
        public VersionDocumentRetrieveException()
            : base(DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionDocumentRetrieveException"/> class with a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public VersionDocumentRetrieveException(Exception innerException)
            : base(DefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionDocumentRetrieveException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public VersionDocumentRetrieveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}