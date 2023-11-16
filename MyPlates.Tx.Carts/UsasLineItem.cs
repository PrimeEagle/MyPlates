using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyPlates.Tx.Carts
{
    public class UsasLineItem
    {
        private string _fund = string.Empty; // USAS Deposit Fund
        private string _pca = string.Empty; // Program Cost Account or Project Cost Account
        private string _co = string.Empty; // Comptroller Object
        private string _ao = string.Empty; // Agency Object
        private decimal _amount = 0.0M; // The dollar amount that will be debited or credited to the specified account.
        private string _depAgency = string.Empty; // Deposit Agency
        private string _tcode = string.Empty; // USAS Transaction Code
        private string _index = string.Empty; // USAS account field
        private StringBuilder _other = new StringBuilder(); // String field of other USAS or non-USAS data for unique need of agencies.

        public UsasLineItem(UsasLineItemConfiguration config, decimal amount,  string transactionId, string traceNumber)
        {
            _fund = config.UsasDepositFund;
            _pca = config.UsasProjectCostAccount;
            _co = config.UsasComptrollerObject;
            _ao = config.UsasAgencyObject;
            _amount = amount;
            _depAgency = config.UsasDepositAgency;
            _tcode = config.UsasTransactionCode;
            _index = config.UsasIndex;

            _other.AppendFormat("{0}{1}", transactionId, traceNumber);
            if (config.UsasVmc != string.Empty)
                _other.AppendFormat("|VMC*{0}|", config.UsasVmc);
            if (config.UsasVnum != string.Empty)
                _other.AppendFormat("VNUM*{0}|", config.UsasVnum);
        }

        public string Fund
        {
            get { return _fund; }
        }

        public string ProjectCostAccount
        {
            get { return _pca; }
        }

        public string ComptrollerObject
        {
            get { return _co; }
        }

        public Boolean HasComptrollerObject
        {
            get { return _co != string.Empty; }
        }

        public string AgencyObject
        {
            get { return _ao; }
        }

        public Boolean HasAgencyObject
        {
            get { return _ao != string.Empty; }
        }

        public string Amount
        {
            get { return _amount.ToString("0.00"); }
        }

        public string DepositAgency
        {
            get { return _depAgency; }
        }

        public Boolean HasDepositAgency
        {
            get { return _depAgency != string.Empty; }
        }

        public string TransactionCode
        {
            get { return _tcode; }
        }

        public string Index
        {
            get { return _index; }
        }

        public Boolean HasIndex
        {
            get { return _index != string.Empty; }
        }

        public bool HasOtherValues
        {
            get { return _other.Length > 0; }
        }

        public string Other
        {
            get
            {
                return _other.ToString();
            }
        }

    }
}
