﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DustInTheWind.CoolApp.Properties {
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
    internal class ServicesResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ServicesResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DustInTheWind.CoolApp.Properties.ServicesResources", typeof(ServicesResources).Assembly);
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
        ///   Looks up a localized string similar to The gate should be an instance of &quot;FileGate&quot;..
        /// </summary>
        internal static string FileLocationProvider_Error_GateIsNotFileGate {
            get {
                return ResourceManager.GetString("FileLocationProvider_Error_GateIsNotFileGate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Azzul - Error.
        /// </summary>
        internal static string MessagesService_Error_Title {
            get {
                return ResourceManager.GetString("MessagesService_Error_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Azzul - Information.
        /// </summary>
        internal static string MessagesService_Information_Title {
            get {
                return ResourceManager.GetString("MessagesService_Information_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Question.
        /// </summary>
        internal static string MessagesService_Question_Title {
            get {
                return ResourceManager.GetString("MessagesService_Question_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Azzul - Warning.
        /// </summary>
        internal static string MessagesService_Warning_Title {
            get {
                return ResourceManager.GetString("MessagesService_Warning_Title", resourceCulture);
            }
        }
    }
}
