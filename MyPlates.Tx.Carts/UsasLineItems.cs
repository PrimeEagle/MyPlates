using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPlates.Tx.Carts
{
    public class UsasLineItems : List<UsasLineItem>
    {
        public UsasLineItems(decimal TxDotYearlyPlateFeeTotal, decimal TxDotGeneralRevenueTotal, decimal EPayFeeTotal, decimal MyPlatesRevenueTotal, string UniqueTransactionID, string TraceNumber)
        {
            UsasConfigurationSectionHandler config = (UsasConfigurationSectionHandler)System.Configuration.ConfigurationManager.GetSection("UsasSettings");
            this.Add(new UsasLineItem(config.EPayIn, EPayFeeTotal, UniqueTransactionID, TraceNumber));
            this.Add(new UsasLineItem(config.EPayOut, EPayFeeTotal, UniqueTransactionID, TraceNumber));
            this.Add(new UsasLineItem(config.MyPlatesIn, MyPlatesRevenueTotal, UniqueTransactionID, TraceNumber));
            this.Add(new UsasLineItem(config.MyPlatesOut, MyPlatesRevenueTotal, UniqueTransactionID, TraceNumber));
            this.Add(new UsasLineItem(config.TxDotGeneralRevenue, TxDotGeneralRevenueTotal, UniqueTransactionID, TraceNumber));
            this.Add(new UsasLineItem(config.TxDotYearlyPlateFee, TxDotYearlyPlateFeeTotal, UniqueTransactionID, TraceNumber));

        }

        public string GetPostParameters()
        {
            StringBuilder postParameters = new StringBuilder();

            for (int i = 1; i <= this.Count; i++)
            {
                UsasLineItem item = this[i-1];
                postParameters.AppendFormat("USAS{0}FUND={1}&", i.ToString(), item.Fund);
                postParameters.AppendFormat("USAS{0}PCA={1}&", i.ToString(), item.ProjectCostAccount);
                if(item.HasComptrollerObject)
                    postParameters.AppendFormat("USAS{0}CO={1}&", i.ToString(), item.ComptrollerObject);
                if(item.HasAgencyObject)
                    postParameters.AppendFormat("USAS{0}AO={1}&", i.ToString(), item.AgencyObject);
                postParameters.AppendFormat("USAS{0}AMOUNT={1}&", i.ToString(), item.Amount);
                if(item.HasDepositAgency)
                    postParameters.AppendFormat("USAS{0}DEPAGENCY={1}&", i.ToString(), item.DepositAgency);
                postParameters.AppendFormat("USAS{0}TCODE={1}&", i.ToString(), item.TransactionCode);
                if(item.HasIndex)
                    postParameters.AppendFormat("USAS{0}INDEX={1}&", i.ToString(), item.Index);
                postParameters.AppendFormat("USAS{0}OTHER={1}&", i.ToString(), item.Other);

            }

            postParameters.AppendFormat("USASLINES={0}", this.Count.ToString());

            return postParameters.ToString();
        }
    }
}
