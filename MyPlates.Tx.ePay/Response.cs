using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyPlates.Tx.ePay
{
    [Serializable]
    public class Response
    {
        private List<string> _responseLines;
        private ResponseType _responseType;
        private string _authorization;
        private string _authorizationCode;
        private string _primaryReturnCode;
        private string _secondaryReturnCode;
        private string _traceNumber;
        private string _hashAlgorithm;
        private string _hashValue;
        private string _batchID;
        private DateTime _transactionDateTime;
        private string _orderID;
        private string _avsResponse;
        private string _cvvReturnCode;

        public Response(StreamReader reader)
        {
            _responseLines = new List<string>();
            while (!reader.EndOfStream)
            {
                _responseLines.Add(reader.ReadLine());
            }

            SetResponseType();

            switch (this._responseType)
            {
                case ResponseType.Approved:
                case ResponseType.Denied:
                    _authorization = FindReturnCode("AUTHORIZATION:");
                    _authorizationCode = FindReturnCode("Auth Code:");
                    _primaryReturnCode = FindReturnCode("Primary RC:");
                    _secondaryReturnCode = FindReturnCode("Secondary RC:");
                    _traceNumber = FindReturnCode("Trace Number:");
                    _hashAlgorithm = FindReturnCode("Hash Algorithm:");
                    _hashValue = FindReturnCode("Hash Value:");
                    string _transactionDate = FindReturnCode("Transaction Date:");
                    string _transactionTime = FindReturnCode("Transaction Time:");
                    _transactionDateTime = new DateTime(int.Parse(_transactionDate.Substring(4, 4)), int.Parse(_transactionDate.Substring(0, 2)), int.Parse(_transactionDate.Substring(2, 2)), int.Parse(_transactionTime.Substring(0, 2)), int.Parse(_transactionTime.Substring(2, 2)), int.Parse(_transactionTime.Substring(4, 2)));
                    _batchID = FindReturnCode("Batch ID:");
                    _orderID = FindReturnCode("Order ID:");
                    _avsResponse = FindReturnCode("AVS Response:");
                    _cvvReturnCode = FindReturnCode("CV RC:");
                    break;
                case ResponseType.InvalidRequest:
                    break;
                case ResponseType.SystemError:
                    break;
                default:
                    break;
            }
                        
        }

        private void SetResponseType()
        {
            _responseType = ResponseType.SystemError;

            foreach (string line in _responseLines)
            {
                if (line.ToUpper() == "APPROVED")
                {
                    _responseType = ResponseType.Approved;
                    break;
                }
                else if (line.ToUpper() == "DENIED")
                {
                    _responseType = ResponseType.Denied;
                    break;
                }
                else if (line.ToUpper() == "INVALID REQUEST")
                {
                    _responseType = ResponseType.InvalidRequest;
                    break;
                }
            }
        }

        public Boolean ValidateResponse()
        {
            StringBuilder hashSeed = new StringBuilder();
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            hashSeed.Append(Merchant.HashSecret);
            hashSeed.Append(_traceNumber.Trim());
            hashSeed.Append(_authorization.Trim());
            hashSeed.Append(_primaryReturnCode);
            hashSeed.Append(_secondaryReturnCode);

            System.Security.Cryptography.SHA1 crypto = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            string encodedHashValue = Convert.ToBase64String(crypto.ComputeHash(enc.GetBytes(hashSeed.ToString())));
            return encodedHashValue == _hashValue;
        }

        private string FindReturnCode(string code)
        {
            foreach (string line in _responseLines)
            {
                if (line.ToUpper().IndexOf(code.ToUpper()) >= 0)
                {
                    return line.Substring(line.IndexOf(':') + 1);
                }
            }
            return string.Empty;
        }

        public ResponseType ResponseType
        {
            get { return _responseType; }
        }

        public string Authorization
        {
            get { return _authorization; }
        }

        public string AuthorizationCode
        {
            get { return _authorizationCode; }
        }

        public PrimaryReturnCode PrimaryReturnCode
        {
            get { return (PrimaryReturnCode)int.Parse(_primaryReturnCode); }
        }

        public string PrimaryReturnCodeString
        {
            get { return _primaryReturnCode; }
        }

        public string SecondaryReturnCode
        {
            get { return _secondaryReturnCode; }
        }

        public string TraceNumber
        {
            get { return _traceNumber; }
        }

        public string BatchID
        {
            get { return _batchID; }
        }

        public DateTime TransactionTimestamp
        {
            get { return _transactionDateTime; }
        }

        public string OrderId
        {
            get { return _orderID; }
        }

        public string HashValue
        {
            get { return _hashValue; }
        }

        public string RawHtml
        {
            get
            {
                StringBuilder results = new StringBuilder();
                foreach (string line in _responseLines)
                {
                    results.Append(line + System.Environment.NewLine);
                }

                return results.ToString();
            }
        }
    }

    public enum ResponseType
    {
        Approved,
        Denied,
        InvalidRequest,
        SystemError
    }

    public enum PrimaryReturnCode
    {
        AuthorizationApproved = 0,
        CommunicationProblem = 1,
        GatewayError = 2,
        SystemError = 3,
        AuthorizationDenied = 4,
        TransactionStateError = 5,
        SystemError99 = 99
    }
}
