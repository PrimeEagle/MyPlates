using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace MyPlates.Tx.ePay
{
    [Serializable]
    public class Payment
    {
        private CreditCard _creditCardInfo;
        private string _cost = "0.00";
        private string _convFee = "0.00";
        private int _customerID;
        private string _traceNumber;
        private Guid _uniqueTransactionID;
        private string _description;
        private string _usasLineItems;

        private Response _response;

        public CreditCard CreditCardInfo
        {
            get { return _creditCardInfo; }
        }

        public decimal Cost
        {
            get { return decimal.Parse(_cost); }
            set { _cost = value.ToString("0.00"); }
        }

        public decimal ConvFee
        {
            get { return decimal.Parse(_convFee); }
            set { _convFee = value.ToString("0.00"); }
        }

        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public string TraceNumber
        {
            get { return _traceNumber; }
            set { _traceNumber = value; }
        }

        public Guid UniqueTransactionID
        {
            get
            {
                if (_uniqueTransactionID == Guid.Empty)
                    _uniqueTransactionID = Guid.NewGuid();
                return _uniqueTransactionID;
            }

            set { _uniqueTransactionID = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string UsasLineItems
        {
            get { return _usasLineItems; }
            set { _usasLineItems = value; }
        }

        public Payment()
        {
            _creditCardInfo = new CreditCard();
        }

        public Payment(CreditCard creditCard)
        {
            _creditCardInfo = creditCard;
        }

        private string GetHash()
        {
            StringBuilder hashSeed = new StringBuilder();
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            hashSeed.Append(Merchant.HashSecret);
            hashSeed.Append(_cost);
            hashSeed.Append(_customerID.ToString());
            hashSeed.Append(_traceNumber);
            hashSeed.Append(Merchant.VendorID.ToString());
            hashSeed.Append(_creditCardInfo.PaymentType);
            hashSeed.Append(_description);

            System.Security.Cryptography.SHA1 crypto = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            return Convert.ToBase64String(crypto.ComputeHash(enc.GetBytes(hashSeed.ToString())));

        }

        private Dictionary<string, string> GetPostItems()
        {
            Dictionary<string, string> q = new Dictionary<string, string>();
            q.Add("PAYTYPE", _creditCardInfo.PaymentType);
            q.Add("VID", Merchant.VendorID.ToString());
            q.Add("CID", _customerID.ToString());
            q.Add("AMOUNT", _cost);
            q.Add("CONVFEE", _convFee);
            q.Add("DESCRIPTION", _description);
            q.Add("CARDTYPE", _creditCardInfo.CardType);
            q.Add("CARDNUMBER", _creditCardInfo.Number);
            q.Add("CARDVERIFYCODE", _creditCardInfo.CVV); //Added CVV number to the query string
            q.Add("EXPIRATIONMONTH", _creditCardInfo.ExpMonth.ToString("00"));
            q.Add("EXPIRATIONYEAR", _creditCardInfo.ExpYear.ToString("0000"));
            
            string address2 = string.Empty;
            if(_creditCardInfo.BillingAddress2 != string.Empty) {
                address2 = ", " + _creditCardInfo.BillingAddress2;
            }

            q.Add("ADDRESS", _creditCardInfo.BillingAddress1 + address2);
            q.Add("CITY", _creditCardInfo.BillingCity);
            q.Add("BILLNAME", _creditCardInfo.Name);
            q.Add("STATE", _creditCardInfo.BillingState);

            string zip = _creditCardInfo.BillingZipCode;
            if (_creditCardInfo.BillingZipCode4.Length > 0)
            {
                zip = zip + "-" + _creditCardInfo.BillingZipCode4;
            }
            q.Add("ZIPCODE", zip);
            q.Add("COUNTRY", _creditCardInfo.BillingCountry);
            q.Add("TRACENUMBER", _traceNumber);
            q.Add("HASHALGORITHM", Merchant.HashAlgorithm);
            q.Add("HASHVALUE", GetHash());
            q.Add("UNIQUETRANSID", Convert.ToBase64String(UniqueTransactionID.ToByteArray()));
            return q;
        }

        public Response ProcessPayment()
        {
            HttpWebRequest epayRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ePayURL"]);
            epayRequest.Method = "POST";
            epayRequest.ContentType = "application/x-www-form-urlencoded";

            StringBuilder postValues = new StringBuilder();

            Dictionary<string, object> logParameters = new Dictionary<string, object>();

            foreach (KeyValuePair<string, string> item in GetPostItems())
            {
                postValues.Append(string.Format("{0}={1}&", item.Key, HttpUtility.UrlEncode(item.Value)));
            }

            postValues.Append(_usasLineItems);

            logParameters.Add("PAYTYPE", _creditCardInfo.PaymentType);
            logParameters.Add("VID", Merchant.VendorID.ToString());
            logParameters.Add("CID", _customerID.ToString());
            logParameters.Add("AMOUNT", _cost);
            logParameters.Add("DESCRIPTION", _description);
            logParameters.Add("CARDTYPE", _creditCardInfo.CardType);
            logParameters.Add("EXPIRATIONMONTH", _creditCardInfo.ExpMonth.ToString("00"));
            logParameters.Add("EXPIRATIONYEAR", _creditCardInfo.ExpYear.ToString("0000"));

            string address2 = string.Empty;
            if (_creditCardInfo.BillingAddress2 != string.Empty)
            {
                address2 = ", " + _creditCardInfo.BillingAddress2;
            }
            logParameters.Add("ADDRESS", _creditCardInfo.BillingAddress1 + address2);
            logParameters.Add("CITY", _creditCardInfo.BillingCity);
            logParameters.Add("BILLNAME", _creditCardInfo.Name);
            logParameters.Add("STATE", _creditCardInfo.BillingState);
            
            string zip = _creditCardInfo.BillingZipCode;
            if (_creditCardInfo.BillingZipCode4.Length > 0)
            {
                zip = zip + "-" + _creditCardInfo.BillingZipCode4;
            }
            logParameters.Add("ZIPCODE", zip);
            logParameters.Add("COUNTRY", _creditCardInfo.BillingCountry);
            logParameters.Add("TRACENUMBER", _traceNumber);
            logParameters.Add("UNIQUETRANSID", Convert.ToBase64String(UniqueTransactionID.ToByteArray()));

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postData = encoding.GetBytes(postValues.ToString().TrimEnd('&'));

            epayRequest.ContentLength = postData.Length;


            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            wsLog.Title = "ePay WebService";
            wsLog.Message = "ProcessPayment()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                using (Stream newStream = epayRequest.GetRequestStream())
                {
                    newStream.Write(postData, 0, postData.Length);

                    using (HttpWebResponse results = (HttpWebResponse)epayRequest.GetResponse())
                    {
                        using (Stream webresponse = results.GetResponseStream())
                        {
                            Encoding encode = Encoding.GetEncoding("utf-8");
                            StreamReader reader = new StreamReader(webresponse, encode);

                            _response = new Response(reader);
                        }
                    }
                }
                logParameters.Add("RAW_HTML", _response.RawHtml == null ? string.Empty : _response.RawHtml);

                if (_response.ResponseType == ResponseType.Approved || _response.ResponseType == ResponseType.Denied)
                {
                    logParameters.Add("AUTHORIZATION", _response.Authorization == null ? string.Empty : _response.Authorization);
                    logParameters.Add("AUTHORIZATION_CODE", _response.AuthorizationCode == null ? string.Empty : _response.AuthorizationCode);
                    logParameters.Add("BATCH_ID", _response.BatchID == null ? string.Empty : _response.BatchID);
                    logParameters.Add("ORDERID", _response.OrderId == null ? string.Empty : _response.OrderId);
                    logParameters.Add("PRIMARY_RETURN_CODE", _response.PrimaryReturnCode);
                    logParameters.Add("PRIMARY_RETURN_CODE_STRING", _response.PrimaryReturnCodeString == null ? string.Empty : _response.PrimaryReturnCodeString);
                    logParameters.Add("RESPONSE_TYPE", _response.ResponseType.ToString() == null ? string.Empty : _response.ResponseType.ToString());
                    logParameters.Add("SECONDARY_RETURN_CODE", _response.SecondaryReturnCode == null ? string.Empty : _response.SecondaryReturnCode);
                    logParameters.Add("TRACE_NUMBER", _response.TraceNumber == null ? string.Empty : _response.TraceNumber);
                    logParameters.Add("TRANSACTION_TIMESTAMP", _response.TransactionTimestamp.ToString());
                }
            }
            catch(Exception exc)
            {
                logParameters.Add("PRIMARY_RETURN_CODE", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                logParameters.Add("PRIMARY_RETURN_CODE_STRING", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = logParameters;
                Logger.Write(wsLog);
            }

            return _response;
        }
    }
}
