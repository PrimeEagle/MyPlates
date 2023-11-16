using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace MyPlates.Tx.ePay
{
    [Serializable()]
    public class CreditCard
    {
        private string _paymentType = ConfigurationManager.AppSettings["DefaultPaymentType"];
        private string _billingCountry = ConfigurationManager.AppSettings["DefaultBillingCountry"];

        private string _billingAddress1;
        private string _billingAddress2;
        private string _billingCity;
        private string _billingState;
        private string _billingZipCode;
        private string _billingZipCode4;

        private int _expMonth;
        private int _expYear;
        private string _number;
        private string _name;
        private string _cvv;
        private string _cardType;
        private string _defaultSymmetricProvider = ConfigurationManager.AppSettings["DefaultSymmetricProvider"];

        public string BillingAddress1
        {
            get { return _billingAddress1; }
            set { _billingAddress1 = value; }
        }

        public string BillingAddress2
        {
            get { return _billingAddress2; }
            set { _billingAddress2 = value; }
        }

        public string BillingCity
        {
            get { return _billingCity; }
            set { _billingCity = value; }
        }

        public string BillingState
        {
            get { return _billingState; }
            set { _billingState = value; }
        }

        public string BillingZipCode
        {
            get { return _billingZipCode; }
            set { _billingZipCode = value; }
        }

        public string BillingZipCode4
        {
            get { return _billingZipCode4; }
            set { _billingZipCode4 = value; }
        }

        public string BillingCountry
        {
            get { return _billingCountry; }
        }

        public int ExpMonth
        {
            get { return _expMonth; }
            set { _expMonth = value; }
        }

        public int ExpYear
        {
            get { return _expYear; }
            set { _expYear = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Number
        {
            get { return this.DecryptInfo(_number); }
            set { _number = this.EncryptInfo(value); }
        }

        public string MaskedNumber
        {
            get 
            {
                return this.DecryptInfo(_number).Substring(this.DecryptInfo(_number).Length - 4, 4).PadLeft(this.DecryptInfo(_number).Length, 'X');
            }
        }

        public string CVV
        {
            get { return this.DecryptInfo(_cvv); }
            set { _cvv = this.EncryptInfo(value); }
        }

        public string CardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }

        public string PaymentType
        {
            get { return _paymentType; }
        }

        private string EncryptInfo(string plainText)
        {
            string encryptedText = "";

            encryptedText = Cryptographer.EncryptSymmetric(_defaultSymmetricProvider, plainText);

            return encryptedText;
        }

        private string DecryptInfo(string encryptedText)
        {
            string plainText = "";

            plainText = Cryptographer.DecryptSymmetric(_defaultSymmetricProvider, encryptedText);

            return plainText;
        }
    }
}
