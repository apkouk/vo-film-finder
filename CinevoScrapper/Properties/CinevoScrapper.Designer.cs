﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinevoScrapper.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.5.0.0")]
    internal sealed partial class CinevoScrapper : global::System.Configuration.ApplicationSettingsBase {
        
        private static CinevoScrapper defaultInstance = ((CinevoScrapper)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new CinevoScrapper())));
        
        public static CinevoScrapper Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("..\\..\\HtmlFiles\\TownScrapper\\")]
        public string TownScrapper {
            get {
                return ((string)(this["TownScrapper"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("..\\..\\HtmlFiles\\TownScrapper\\Processed")]
        public string TownScrapperProcessed {
            get {
                return ((string)(this["TownScrapperProcessed"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("..\\..\\HtmlFiles\\CinemaScrapper\\")]
        public string CinemaScrapper {
            get {
                return ((string)(this["CinemaScrapper"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("..\\..\\HtmlFiles\\CinemaScrapper\\Processed")]
        public string CinemaScrapperProcessed {
            get {
                return ((string)(this["CinemaScrapperProcessed"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CleanDirectories {
            get {
                return ((bool)(this["CleanDirectories"]));
            }
        }
    }
}
