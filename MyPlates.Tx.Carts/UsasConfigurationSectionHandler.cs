using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MyPlates.Tx.Carts
{
    public class UsasConfigurationSectionHandler : ConfigurationSection
    {
        public UsasConfigurationSectionHandler()
        {
        }

        [ConfigurationProperty("ePayIn")]
        public UsasLineItemConfiguration EPayIn
        {
            get { return (UsasLineItemConfiguration)this["ePayIn"]; }
        }

        [ConfigurationProperty("ePayOut")]
        public UsasLineItemConfiguration EPayOut
        {
            get { return (UsasLineItemConfiguration)this["ePayOut"]; }
        }
        
        [ConfigurationProperty("MyPlatesIn")]
        public UsasLineItemConfiguration MyPlatesIn
        {
            get { return (UsasLineItemConfiguration)this["MyPlatesIn"]; }
        }
        
        [ConfigurationProperty("MyPlatesOut")]
        public UsasLineItemConfiguration MyPlatesOut
        {
            get { return (UsasLineItemConfiguration)this["MyPlatesOut"]; }
        }
        
        [ConfigurationProperty("TxDotGeneralRevenue")]
        public UsasLineItemConfiguration TxDotGeneralRevenue
        {
            get { return (UsasLineItemConfiguration)this["TxDotGeneralRevenue"]; }
        }
        
        [ConfigurationProperty("TxDotYearlyPlateFee")]
        public UsasLineItemConfiguration TxDotYearlyPlateFee
        {
            get { return (UsasLineItemConfiguration)this["TxDotYearlyPlateFee"]; }
        }
    }

    public class UsasLineItemConfiguration : ConfigurationElement
    {
        public UsasLineItemConfiguration()
        {
        }

        [ConfigurationProperty("UsasDepositFund",DefaultValue= "", IsRequired=true)]
        [StringValidator(MinLength = 0, MaxLength = 4)]
        public string UsasDepositFund
        {
            get { return (string)this["UsasDepositFund"]; }
        }

        [ConfigurationProperty("UsasProjectCostAccount", DefaultValue = "", IsRequired = true)]
        [StringValidator(MinLength = 0, MaxLength = 5)]
        public string UsasProjectCostAccount
        {
            get { return (string)this["UsasProjectCostAccount"]; }
        }

        [ConfigurationProperty("UsasComptrollerObject", DefaultValue = "", IsRequired = true)]
        [StringValidator(MinLength = 0, MaxLength = 4)]
        public string UsasComptrollerObject
        {
            get { return (string)this["UsasComptrollerObject"]; }
        }

        [ConfigurationProperty("UsasAgencyObject", DefaultValue = "", IsRequired = true)]
        [StringValidator(MinLength = 0, MaxLength = 4)]
        public string UsasAgencyObject
        {
            get { return (string)this["UsasAgencyObject"]; }
        }

        [ConfigurationProperty("UsasDepositAgency", DefaultValue = "", IsRequired = false)]
        [StringValidator(MinLength = 0, MaxLength = 3)]
        public string UsasDepositAgency
        {
            get { return (string)this["UsasDepositAgency"]; }
        }

        [ConfigurationProperty("UsasTransactionCode", DefaultValue = "", IsRequired = true)]
        [StringValidator(MinLength = 0, MaxLength = 3)]
        public string UsasTransactionCode
        {
            get { return (string)this["UsasTransactionCode"]; }
        }

        [ConfigurationProperty("UsasIndex", DefaultValue = "", IsRequired = false)]
        [StringValidator(MinLength = 0, MaxLength = 5)]
        public string UsasIndex
        {
            get { return (string)this["UsasIndex"]; }
        }

        [ConfigurationProperty("UsasVmc", DefaultValue = "", IsRequired = false)]
        [StringValidator(MinLength = 0, MaxLength = 1000)]
        public string UsasVmc
        {
            get { return (string)this["UsasVmc"]; }
        }

        [ConfigurationProperty("UsasVnum", DefaultValue = "", IsRequired = false)]
        [StringValidator(MinLength = 0, MaxLength = 1000)]
        public string UsasVnum
        {
            get { return (string)this["UsasVnum"]; }
        }
    }
}
