﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DustInTheWind.Azzul.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ApplicationLoadingResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApplicationLoadingResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DustInTheWind.Azzul.Properties.ApplicationLoadingResources", typeof(ApplicationLoadingResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading the catalogs....
        /// </summary>
        internal static string CatalogsTask_StatusText_Default {
            get {
                return ResourceManager.GetString("CatalogsTask_StatusText_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading the catalogs... {0}.
        /// </summary>
        internal static string CatalogsTask_StatusText_Detailed {
            get {
                return ResourceManager.GetString("CatalogsTask_StatusText_Detailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parsing command line arguments....
        /// </summary>
        internal static string CommandLineArgumentsTask_StatusText {
            get {
                return ResourceManager.GetString("CommandLineArgumentsTask_StatusText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reading configuration file....
        /// </summary>
        internal static string ConfigurationTask_StatusText {
            get {
                return ResourceManager.GetString("ConfigurationTask_StatusText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The gates directory does not exist and could not be created: {0}.
        /// </summary>
        internal static string GatesTask_Error_GateDirectoryDoesNotExists {
            get {
                return ResourceManager.GetString("GatesTask_Error_GateDirectoryDoesNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Gate {0} from assembly {1} does not have a parameterless constructor..
        /// </summary>
        internal static string GatesTask_Error_NoParameterlessConstructor {
            get {
                return ResourceManager.GetString("GatesTask_Error_NoParameterlessConstructor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading the gates....
        /// </summary>
        internal static string GatesTask_StatusText {
            get {
                return ResourceManager.GetString("GatesTask_StatusText", resourceCulture);
            }
        }
    }
}
