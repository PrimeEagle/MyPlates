﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TxDot.Web.Services.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://wt-rts-ts1:8583/RTSPOSProject/services/RtsAdmService")]
        public string MyPlates_Tx_TxDot_WebServices_Proxy_RtsAdmService_RtsAdmServiceService {
            get {
                return ((string)(this["MyPlates_Tx_TxDot_WebServices_Proxy_RtsAdmService_RtsAdmServiceService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://wt-rts-ts1:8583/RTSPOSProject/services/RtsInvService")]
        public string MyPlates_Tx_TxDot_WebServices_Proxy_RtsInvService_RtsInvServiceService {
            get {
                return ((string)(this["MyPlates_Tx_TxDot_WebServices_Proxy_RtsInvService_RtsInvServiceService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://wt-rts-ts1:8583/RTSPOSProject/services/RtsTransService")]
        public string MyPlates_Tx_TxDot_WebServices_Proxy_RtsTransService_RtsTransServiceService {
            get {
                return ((string)(this["MyPlates_Tx_TxDot_WebServices_Proxy_RtsTransService_RtsTransServiceService"]));
            }
        }
    }
}