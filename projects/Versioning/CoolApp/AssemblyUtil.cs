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
using DustInTheWind.CoolApp.Properties;

namespace DustInTheWind.CoolApp
{
    /// <summary>
    /// Reads different meta information from an assembly.
    /// </summary>
    public static class AssemblyUtil
    {
        /// <summary>
        /// Returns the informational version for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the informational version.</param>
        /// <returns>The informational version of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetAssemblyInformationalVersion(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;

                AssemblyInformationalVersionAttribute attribute = (AssemblyInformationalVersionAttribute)attributes[0];
                return attribute.InformationalVersion;
            }
            catch (Exception ex)
            {
                string fullAssemblyName = assembly.GetName().FullName;
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_InformationalVersion_Error, fullAssemblyName);
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
        public static string GetAssemblyTitle(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                return attributes.Length > 0
                    ? ((AssemblyTitleAttribute)attributes[0]).Title
                    : Path.GetFileNameWithoutExtension(assembly.CodeBase);
            }
            catch (Exception ex)
            {
                string fullAssemblyName = assembly.GetName().FullName;
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_AssemblyTitle_Error, fullAssemblyName);
                throw new AzzulException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Returns the version for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the version.</param>
        /// <returns>The version of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        public static Version GetAssemblyVersion(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            return assembly.GetName().Version;
        }

        /// <summary>
        /// Returns the description for a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which to return the description.</param>
        /// <returns>The description of the assembly.</returns>
        /// <exception cref="ArgumentNullException">The assembly parameter is null.</exception>
        /// <exception cref="AzzulException">Could not retrieve the requested information.</exception>
        public static string GetAssemblyDescription(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;

                AssemblyDescriptionAttribute attribute = (AssemblyDescriptionAttribute)attributes[0];
                return attribute.Description;
            }
            catch (Exception ex)
            {
                string fullAssemblyName = assembly.GetName().FullName;
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_AssemblyDescription_Error, fullAssemblyName);
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
        public static string GetProductName(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;

                AssemblyProductAttribute attribute = (AssemblyProductAttribute)attributes[0];
                return attribute.Product;
            }
            catch (Exception ex)
            {
                string fullAssemblyName = assembly.GetName().FullName;
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_ProductName_Error, fullAssemblyName);
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
        public static string GetCopyrightInformation(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;

                AssemblyCopyrightAttribute attribute = (AssemblyCopyrightAttribute)attributes[0];
                return attribute.Copyright;
            }
            catch (Exception ex)
            {
                string fullAssemblyName = assembly.GetName().FullName;
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_CopyrightInformation_Error, fullAssemblyName);
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
        public static string GetCompanyName(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            try
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;

                AssemblyCompanyAttribute attribute = (AssemblyCompanyAttribute)attributes[0];
                return attribute.Company;
            }
            catch (Exception ex)
            {
                string fullAssemblyName = assembly.GetName().FullName;
                string errorMessage = string.Format(AssemblyUtilResources.AssemblyUtil_CompanyName_Error, fullAssemblyName);
                throw new AzzulException(errorMessage, ex);
            }
        }
    }
}