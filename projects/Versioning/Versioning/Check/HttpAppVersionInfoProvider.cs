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
using System.IO;
using System.Net;

namespace DustInTheWind.Versioning.Check
{
    /// <summary>
    /// Creates <see cref="AppVersionInfo"/> objects with data retrieved from a file.
    /// </summary>
    public class HttpAppVersionInfoProvider : IAppVersionInfoProvider
    {
        public string Url { get; set; }

        public string AppName { get; set; }

        public string Location
        {
            get { return Url; }
        }

        public AppVersionInfo GetVersionInformation()
        {
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(Url);
                myRequest.Method = "GET";

                using (WebResponse myResponse = myRequest.GetResponse())
                {
                    using (Stream stream = myResponse.GetResponseStream())
                    {
                        return AppInfoFileParser.GetAppInfo(stream, AppName);
                    }
                }
            }
            catch (VersionCheckingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new VersionCheckingException(ex);
            }
        }
    }
}