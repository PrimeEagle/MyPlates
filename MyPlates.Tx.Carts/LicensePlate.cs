using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using TxDot.Web.Services;
using MyPlates.Tx.Data;

namespace MyPlates.Tx.Carts
{
    [Serializable()]
    public class LicensePlate : IBuyable
    {
        private string _mfgText;
        private PersonalInfo _ownerInfo = new PersonalInfo(ConfigurationManager.AppSettings["DefaultState"]);
        private string _plateCode;
        private int _renewalPeriod;
        private int _categoryId;
        private decimal _totalCost;
        private decimal _txdotYearlyPlateFee;
        private decimal _txdotGeneralRevenuePercentage;
        private DateTime _timestamp;
        private bool _reservedForMyPlates;
        private string _traceNumber;
        private string _categoryName;
        private string _typeName;
        private Guid _itemId;
        private string _class;
        private string _suggestionsAlgorithm;
        private int _numSuggestions;
        private bool _validateSuggestions;
        private string _regEx;
        private string _regExISA;
        private byte[] _plateImage;
        private int _imageSizeX;
        private int _imageSizeY;
        private string _countyStreet1;
        private string _countyStreet2;
        private string _countyCity;
        private string _countyState;
        private string _countyZIP;
        private string _countyPhone;
        private string _countyEmail;

        public string CountyEmail
        {
            get { return _countyEmail; }
            set { _countyEmail = value; }
        }


        public string CountyPhone
        {
            get { return _countyPhone; }
            set { _countyPhone = value; }
        }

        public string CountyStreet2
        {
            get { return _countyStreet2; }
            set { _countyStreet2 = value; }
        }

        public string CountyStreet1
        {
            get { return _countyStreet1; }
            set { _countyStreet1 = value; }
        }

        public string CountyCity
        {
            get { return _countyCity; }
            set { _countyCity = value; }
        }

        public string CountyZIP
        {
            get { return _countyZIP; }
            set { _countyZIP = value; }
        }

        public string CountyState
        {
            get { return _countyState; }
            set { _countyState = value; }
        }

        public int ImageSizeY
        {
            get { return _imageSizeY; }
            set { _imageSizeY = value; }
        }

        public int ImageSizeX
        {
            get { return _imageSizeX; }
            set { _imageSizeX = value; }
        }

        public string SuggestionsAlgorithm
        {
            get { return _suggestionsAlgorithm; }
            set { _suggestionsAlgorithm = value; }
        }

        public int NumSuggestions
        {
            get { return _numSuggestions; }
            set { _numSuggestions = value; }
        }

        public bool ValidateSuggestions
        {
            get { return _validateSuggestions; }
            set { _validateSuggestions = value; }
        }

        public string Text
        {
            get { return Regex.Replace(_mfgText.ToUpper(), @"[^\w-]+", ""); }
        }

        public byte[] PlateImage
        {
            get { return _plateImage; }
            set { _plateImage = value; }
        }

        public string MfgText
        {
            get 
            {
                return _mfgText;
            }

            set 
            {
                _mfgText = value.ToUpper();
                GenerateNewTraceNumber();
            }
        }

        public Guid ItemID
        {
            get { return _itemId; }
        }

        public void GenerateNewTraceNumber()
        {
            _traceNumber = (ConfigurationManager.AppSettings["TraceNumLicensePlatePrefix"].ToString() + this.Text + this.GetRandomAlphanumeric(3)).ToUpper();
        }

        public PersonalInfo OwnerInfo
        {
            get { return _ownerInfo; }
        }

        public string PlateCode
        {
            get { return _plateCode; }
            set { _plateCode = value; }
        }

        public int RenewalPeriod
        {
            get { return _renewalPeriod; }
            set { _renewalPeriod = value; }
        }

        public int CategoryID
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public string RegEx
        {
            get { return _regEx; }
            set { _regEx = value; }
        }

        public string RegExISA
        {
            get { return _regExISA; }
            set { _regExISA = value; }
        }

        public bool ISA
        {
            get 
            {
                if (_mfgText.IndexOf(ConfigurationManager.AppSettings["ISASymbol"]) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public decimal TotalCost
        {
            get { return _totalCost; }
            set { _totalCost = value; }
        }

        public decimal TxDotYearlyPlateFee
        {
            get { return _txdotYearlyPlateFee; }
            set { _txdotYearlyPlateFee = value; }
        }

        public decimal TxDotGeneralRevenuePercentage
        {
            get { return _txdotGeneralRevenuePercentage; }
            set { _txdotGeneralRevenuePercentage = value; }
        }

        public decimal TxDotGeneralRevenueCost
        {
            get
            {
                return Decimal.Round(((_totalCost - (_txdotYearlyPlateFee * _renewalPeriod)) * (_txdotGeneralRevenuePercentage / 100)), 2, MidpointRounding.AwayFromZero);
            }
        }

        public decimal TxDotYearlyPlateFeeCost
        {
            get
            {
                return Decimal.Round(_txdotYearlyPlateFee, 2, MidpointRounding.AwayFromZero);
            }
        }

        public decimal myPlatesRevenueCost
        {
            get
            {
                return _totalCost - TxDotGeneralRevenueCost - TxDotYearlyPlateFeeCost;
            }
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }
        }

        public bool ValidPattern
        {
            get
            {
                Regex patternRegEx;
                if (this.ISA)
                {
                    patternRegEx = new Regex(_regExISA);
                }
                else
                {
                    patternRegEx = new Regex(_regEx);
                }

                return patternRegEx.IsMatch(this.Text);
            }
        }

        public bool ReservedForMyPlates
        {
            get { return _reservedForMyPlates; }
            set { _reservedForMyPlates = value; }
        }

        public string TraceNumber
        {
            get { return _traceNumber; }
        }

        public LicensePlate()
        {
            _itemId = Guid.NewGuid();
            _timestamp = DateTime.Now;
        }

        public TxDotResponse ProcessOrder(string traceNumber, int orderId, DateTime ePaySendTime, DateTime ePayReceiveTime)
        {
            TxDotResponse response;

            if (_reservedForMyPlates)
            {
                response = TxDotWebServices.OrderReservedPlate(MyPlates.Tx.Helper.Context.SessionID, MyPlates.Tx.Helper.Context.Username, _plateCode, this.Text, _mfgText, _ownerInfo.Street1, _ownerInfo.Street2,
                            _ownerInfo.City, _ownerInfo.State, _ownerInfo.ZIP, _ownerInfo.ZIP4, _ownerInfo.CountyNumber, _ownerInfo.NameFirst, _ownerInfo.NameLast,
                            _ownerInfo.Phone, _ownerInfo.Email, _renewalPeriod, TotalCost, DateTime.Now, traceNumber, orderId,
                            ePaySendTime, ePayReceiveTime, this.ISA);

                MyPlatesData.RecordResevedPlateOrder(this.Text);
            }
            else
            {
                response = TxDotWebServices.OrderPlate(MyPlates.Tx.Helper.Context.SessionID, MyPlates.Tx.Helper.Context.Username, _plateCode, this.Text, _mfgText, _ownerInfo.Street1, _ownerInfo.Street2,
                            _ownerInfo.City, _ownerInfo.State, _ownerInfo.ZIP, _ownerInfo.ZIP4, _ownerInfo.CountyNumber, _ownerInfo.NameFirst, _ownerInfo.NameLast,
                            _ownerInfo.Phone, _ownerInfo.Email, _renewalPeriod, TotalCost, DateTime.Now, traceNumber, orderId,
                            ePaySendTime, ePayReceiveTime);
            }

            return response;
        }

        public decimal GetTotalCost()
        {
            return _totalCost;
        }

        public void CreateOrder(int orderId)
        {
            int ownerId = 0;

            ownerId = MyPlatesData.CreateOwner(_ownerInfo.NameFirst, _ownerInfo.NameLast, _ownerInfo.Street1, _ownerInfo.Street2,
                             _ownerInfo.City, _ownerInfo.State, _ownerInfo.County, _ownerInfo.ZIP, _ownerInfo.ZIP4, _ownerInfo.Email,
                             _ownerInfo.Phone);

            MyPlatesData.CreateOrderPlate(orderId, ownerId, _plateCode, this.Text, _mfgText, _itemId, _plateImage, _renewalPeriod, 
                                                _totalCost, _txdotYearlyPlateFee * _renewalPeriod, _txdotGeneralRevenuePercentage,
                                                 _countyStreet1, _countyStreet2, _countyCity, _countyState, _countyZIP, _countyPhone, _countyEmail);
        }

        private string GetRandomAlphanumeric(int length)
        {
            char[] chars = new char[62];
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[length];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        public string GetTraceNumber()
        {
            return _traceNumber;
        }

        public string GetDescription()
        {
            return this.Text;
        }

        public Guid GetItemId()
        {
            return _itemId;
        }
    }
}