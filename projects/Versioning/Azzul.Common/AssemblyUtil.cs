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
using System.IO;
using System.Reflection;
using DustInTheWind.Azzul.Properties;

namespace DustInTheWind.Azzul
{
    /// <summary>
    /// Reads different meta information from an assembly.
    /// </summary>
    public class AssemblyUtil
    {
        #region Calling Assembly

        /// <summary>
        /// Returns the informational version of the calling assembly.
        /// </summary>
        /// <returns>The informational version of the calling assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetCallingAssemblyInformationalVersion()
        {
            return GetAssemblyInformationalVersion(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Returns the title of the calling assembly.
        /// </summary>
        /// <returns>The title of the calling assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetCallingAssemblyTitle()
        {
            return GetAssemblyTitle(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Returns the version of the calling assembly.
        /// </summary>
        /// <returns>The version of the calling assembly.</returns>
        public static Version GetCallingAssemblyVersion()
        {
            return GetAssemblyVersion(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Returns the description of the calling assembly.
        /// </summary>
        /// <returns>The description of the calling assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetCallingAssemblyDescription()
        {
            return GetAssemblyDescription(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Returns the product name of the calling assembly.
        /// </summary>
        /// <returns>The product name of the calling assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetCallingAssemblyProductName()
        {
            return GetProductName(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Returns the copyright information of the calling assembly.
        /// </summary>
        /// <returns>The copyright information of the calling assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetCallingAssemblyCopyrightInformation()
        {
            return GetCopyrightInformation(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Returns the company name of the calling assembly.
        /// </summary>
        /// <returns>The company name of the calling assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetCallingAssemblyCompanyName()
        {
            return GetCompanyName(Assembly.GetCallingAssembly());
        }

        #endregion

        #region Assembly as parameter

        /// <summary>
        /// Returns the informational version for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the informational version.</param>
        /// <returns>The informational version of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetAssemblyInformationalVersion(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);

                if (attributes.Length > 0)
                    return ((AssemblyInformationalVersionAttribute)attributes[0]).InformationalVersion;

                return string.Empty;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_InformationalVersion_Error, assembly.GetName().FullName);
                throw new AzzulException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Returns the title for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the title.</param>
        /// <returns>The title of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetAssemblyTitle(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                if (attributes.Length > 0)
                    return ((AssemblyTitleAttribute)attributes[0]).Title;

                return Path.GetFileNameWithoutExtension(assembly.CodeBase);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_AssemblyTitle_Error, assembly.GetName().FullName);
                throw new AzzulException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Returns the version for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the version.</param>
        /// <returns>The version of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        public static Version GetAssemblyVersion(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.GetName().Version;
        }

        /// <summary>
        /// Returns the description for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the description.</param>
        /// <returns>The description of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetAssemblyDescription(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

                if (attributes.Length > 0)
                    return ((AssemblyDescriptionAttribute)attributes[0]).Description;

                return string.Empty;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_AssemblyDescription_Error, assembly.GetName().FullName);
                throw new AzzulException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Returns the product name for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the product name.</param>
        /// <returns>The product name of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetProductName(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);

                if (attributes.Length > 0)
                    return ((AssemblyProductAttribute)attributes[0]).Product;

                return string.Empty;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_ProductName_Error, assembly.GetName().FullName);
                throw new AzzulException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Returns the copyright information for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the copyright information.</param>
        /// <returns>The copyright information of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetCopyrightInformation(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                if (attributes.Length > 0)
                    return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

                return string.Empty;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_CopyrightInformation_Error, assembly.GetName().FullName);
                throw new AzzulException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Returns the company name for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the company name.</param>
        /// <returns>The company name of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetCompanyName(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

                if (attributes.Length > 0)
                    return ((AssemblyCompanyAttribute)attributes[0]).Company;

                return string.Empty;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_CompanyName_Error, assembly.GetName().FullName);
                throw new AzzulException(errorMessage, ex);
            }
        }

        #endregion

        #region Instance

        /// <summary>
        /// The assembly object from which to extract information.
        /// </summary>
        private readonly Assembly assembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyUtil"/> class with
        /// the assembly object from which to extract information.
        /// </summary>
        /// <param name="assembly">The assembly object from which to extract information.</param>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        public AssemblyUtil(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            this.assembly = assembly;
        }

        /// <summary>
        /// Returns the informational version of the assembly.
        /// </summary>
        /// <returns>The informational version of the assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public string GetInformationalVersion()
        {
            return GetAssemblyInformationalVersion(assembly);
        }

        /// <summary>
        /// Returns the title of the assembly.
        /// </summary>
        /// <returns>The title of the assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public string GetTitle()
        {
            return GetAssemblyTitle(assembly);
        }

        /// <summary>
        /// Returns the version of the assembly.
        /// </summary>
        /// <returns>The version of the assembly.</returns>
        public Version GetVersion()
        {
            return GetAssemblyVersion(assembly);
        }

        /// <summary>
        /// Returns the description of the assembly.
        /// </summary>
        /// <returns>The description of the assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public string GetDescription()
        {
            return GetAssemblyDescription(assembly);
        }

        /// <summary>
        /// Returns the product name of the assembly.
        /// </summary>
        /// <returns>The product name of the assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public string GetProductName()
        {
            return GetProductName(assembly);
        }

        /// <summary>
        /// Returns the copyright information of the assembly.
        /// </summary>
        /// <returns>The copyright information of the assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public string GetCopyrightInformation()
        {
            return GetCopyrightInformation(assembly);
        }

        /// <summary>
        /// Returns the company name of the assembly.
        /// </summary>
        /// <returns>The company name of the assembly.</returns>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public string GetCompanyName()
        {
            return GetCompanyName(assembly);
        }

        #endregion
    }
}
