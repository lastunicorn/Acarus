using System;
using System.IO;
using System.Xml.XPath;
using DustInTheWind.Versioning.Properties;

namespace DustInTheWind.Versioning
{
    internal class AppInfoFileParser
    {
        /// <summary>
        /// Retrieve the version information from the specified xpath document.
        /// </summary>
        /// <param name="stream">The xpath document containing the data.</param>
        /// <param name="applicationName">The name of the application for which to return the version information.</param>
        /// <returns>An instance of the <see cref="AppVersionInfo"/> class populated with data prom the xpath document.</returns>
        /// <exception cref="VersionCheckingException"></exception>
        public static AppVersionInfo GetAppInfo(Stream stream, string applicationName)
        {
            IXPathNavigable xPathDocument = new XPathDocument(stream);

            if (xPathDocument == null)
                throw new VersionDocumentRetrieveException();

            XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();
            XPathExpression xPathExpression = xPathNavigator.Compile(string.Format("/appInfo/app[@name='{0}']", applicationName));
            XPathNavigator xPathAppNavigator = xPathNavigator.SelectSingleNode(xPathExpression);

            if (xPathAppNavigator == null)
            {
                string message = string.Format(VersioningResources.Error_VersionInformation_NoInformationAboutApplication, applicationName);
                throw new VersionCheckingException(message);
            }

            return ParseAppTag(xPathAppNavigator);
        }

        /// <summary>
        /// Parses the "version" node represented by a <see cref="XPathNavigator"/> object.
        /// </summary>
        /// <param name="xpathNav">The <see cref="XPathNavigator"/> object containing the "version" node.</param>
        /// <returns>An instance of the <see cref="AppVersionInfo"/> class.</returns>
        /// <exception cref="VersionCheckingException"></exception>
        private static AppVersionInfo ParseAppTag(XPathNavigator xpathNav)
        {
            string name = null;
            Version version = null;
            string informationalVersion = null;
            string description = null;
            string downloadUrl = null;

            if (xpathNav.MoveToFirstAttribute())
            {
                do
                {
                    switch (xpathNav.Name)
                    {
                        case "name":
                            name = xpathNav.Value;
                            break;

                        case "version":
                            try
                            {
                                version = new Version(xpathNav.Value);
                            }
                            catch (Exception ex)
                            {
                                throw new VersionCheckingException(VersioningResources.Error_VersionInformation_InvalidVersionsFile_InvalidVersionAttribute, ex);
                            }
                            break;

                        case "informationalVersion":
                            informationalVersion = xpathNav.Value;
                            break;

                        case "description":
                            description = xpathNav.Value;
                            break;

                        case "downloadUrl":
                            downloadUrl = xpathNav.Value;
                            break;
                    }
                }
                while (xpathNav.MoveToNextAttribute());
            }

            if (version == null)
                throw new VersionCheckingException(VersioningResources.Error_VersionInformation_VersionAttributeNotFound);

            return new AppVersionInfo(name, version, informationalVersion, description, downloadUrl);
        }
    }
}