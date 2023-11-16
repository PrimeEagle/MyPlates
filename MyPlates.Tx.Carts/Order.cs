using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MyPlates.Tx.Data;
using MyPlates.Tx.ePay;
using TxDot.Web.Services;
using MyPlates.Tx.Helper;

namespace MyPlates.Tx.Carts
{
    [Serializable()]
    public class Order
    {
        enum OrderStatus { Successful = 1, Denied = 2, Error = 3, NotSent = 4, Invalid = 5 };

        private List<IBuyable> _products = new List<IBuyable>();
        private int _orderId;
        private DateTime _orderTime;
        private bool _completed = false;
        private string _traceNumber = "";
        private string _description = string.Empty;
        private PayerInfo _billingInfo = new PayerInfo();
        private int _customerId;
        private DateTime _ePaySendTime;
        private DateTime _ePayReceiveTime;
        private decimal _ePayFeePercentage;
        private decimal _ePayTransactionFee;
        private Payment ePayPayment;

        public PayerInfo BillingInfo
        {
            get { return _billingInfo; }
        }

        public decimal ePayTransactionFee
        {
            get { return _ePayTransactionFee; }
            set { _ePayTransactionFee = value; }
        }

        public decimal ePayFeePercentage
        {
            get { return _ePayFeePercentage; }
            set { _ePayFeePercentage = value; }
        }

        public int OrderID
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public DateTime OrderTime
        {
            get { return _orderTime; }
            set { _orderTime = value; }
        }

        public bool Completed
        {
            get { return _completed; }
            set { _completed = value; }
        }

        public DateTime ePaySendTime
        {
            get { return _ePaySendTime; }
            set { _ePaySendTime = value; }
        }

        public DateTime ePayReceiveTime
        {
            get { return _ePayReceiveTime; }
            set { _ePayReceiveTime = value; }
        }

        public string TraceNumber
        {
            get
            {
                if (_products.Count == 0)
                {
                    return null;
                }
                else
                {
                    if (_traceNumber == string.Empty)
                    {
                        _traceNumber = _products[0].GetTraceNumber();
                    }

                    return _traceNumber;
                }
            }

            set
            {
                _traceNumber = value;
            }
        }

        public Guid UniqueTransactionID
        {
            get { return this.ePayPayment.UniqueTransactionID; }
            set { this.ePayPayment.UniqueTransactionID = value; }
        }

        public string Description
        {
            get
            {
                if (_products.Count == 0)
                {
                    return null;
                }
                else
                {
                    if (_description == string.Empty)
                    {
                        _description = _products[0].GetDescription();
                    }

                    return _description;
                }
            }
        }

        public decimal CostTotal
        {
            get { return this.CalculateTotalCost(); }
        }

        public List<IBuyable> Products
        {
            get { return _products; }
            set { _products = value; }
        }

        public Order()
        {
            ePayPayment = new Payment(BillingInfo.CreditCardInfo);
            _ePayFeePercentage = Decimal.Parse(ConfigurationManager.AppSettings["EPayFeePercentage"]);
            _ePayTransactionFee = Decimal.Parse(ConfigurationManager.AppSettings["EPayTransactionFee"]);
        }

        private decimal CalculateTotalCost()
        {
            decimal totalCost = 0;
            foreach (IBuyable product in _products)
            {
                totalCost += product.GetTotalCost();
            }

            return totalCost;
        }

        private decimal GetTxDotYearlyPlateFeeTotal()
        {
            decimal total = 0.0M;

            foreach (IBuyable item in _products)
            {
                LicensePlate plate = item as LicensePlate;
                if (plate != null)
                {
                    total += plate.TxDotYearlyPlateFeeCost;
                }
            }

            return total;
        }

        private decimal GetTxDotGeneralRevenueTotal()
        {
            decimal total = 0.0M;

            foreach (IBuyable item in _products)
            {
                LicensePlate plate = item as LicensePlate;
                if (plate != null)
                {
                    total += plate.TxDotGeneralRevenueCost;
                }
            }

            return total;
        }

        private decimal GetEPayFeeTotal()
        {
            return Decimal.Round(CalculateTotalCost() * _ePayFeePercentage / 100 + _ePayTransactionFee, 2, MidpointRounding.AwayFromZero);
        }

        private decimal GetMyPlatesRevenueTotal()
        {
            decimal total = 0.0M;

            foreach (IBuyable item in _products)
            {
                LicensePlate plate = item as LicensePlate;
                if (plate != null)
                {
                    total += plate.myPlatesRevenueCost;
                }
            }

            return total - GetEPayFeeTotal();
        }

        public void RecordOrder()
        {
            _customerId = MyPlatesData.CreateCustomer(this.BillingInfo.NameFirst, this.BillingInfo.NameLast, this.BillingInfo.Street1, this.BillingInfo.Street2,
                this.BillingInfo.City, this.BillingInfo.State, this.BillingInfo.County, this.BillingInfo.ZIP, this.BillingInfo.ZIP4, this.BillingInfo.Email,
                                this.BillingInfo.Phone);

            _orderId = MyPlatesData.CreateOrder(Context.SessionID, _customerId, Context.Username, Context.UserHostAddress, this.TraceNumber);

            foreach (IBuyable product in _products)
            {
                product.CreateOrder(_orderId);
            }
        }

        public void ProcessOrder()
        {
            TxDotResponse response = new TxDotResponse();
            string firstFailureReason = "";
            OrderStatus status = OrderStatus.NotSent;

            DateTime orderStartTime = DateTime.Now;

            foreach (IBuyable product in _products)
            {
                response = product.ProcessOrder(this.TraceNumber, _orderId, _ePaySendTime, _ePayReceiveTime);

                if (response.Success)
                {
                    status = OrderStatus.Successful;
                }
                else
                {
                    status = OrderStatus.Error;

                    if (firstFailureReason == string.Empty)
                    {
                        firstFailureReason = response.ErrorMsg;
                    }
                    break;
                }
            }

            MyPlatesData.SaveTxDotOrderResult(_orderId, Convert.ToInt32(status), orderStartTime, firstFailureReason);
            if (!response.Success)
            {
                throw new Exception("Order failed.");
            }
        }

        public bool FinalizePayment()
        {
            ePayPayment.Cost = this.CostTotal;
            ePayPayment.ConvFee = this.GetEPayFeeTotal();
            ePayPayment.CustomerID = _customerId;
            ePayPayment.TraceNumber = this.TraceNumber;
            ePayPayment.Description = this.Description;
            UsasLineItems usas = new UsasLineItems(this.GetTxDotYearlyPlateFeeTotal(), this.GetTxDotGeneralRevenueTotal(), this.GetEPayFeeTotal(), this.GetMyPlatesRevenueTotal(), ePayPayment.UniqueTransactionID.ToString("N"), ePayPayment.TraceNumber);
            ePayPayment.UsasLineItems = usas.GetPostParameters();

            string failureReason = string.Empty;
            OrderStatus status = OrderStatus.NotSent;

            _ePaySendTime = DateTime.Now;
            Response _response = ePayPayment.ProcessPayment();
            _ePayReceiveTime = DateTime.Now;

            switch (_response.ResponseType)
            {
                case ResponseType.Approved:
                    status = OrderStatus.Successful;
                    break;
                case ResponseType.Denied:
                    failureReason = "Denied.";
                    status = OrderStatus.Denied;
                    break;
                case ResponseType.InvalidRequest:
                    failureReason = "Invalid Request.";
                    status = OrderStatus.Invalid;
                    break;
                case ResponseType.SystemError:
                    failureReason = "System Error.";
                    status = OrderStatus.Error;
                    break;
                default:
                    break;
            }

            MyPlatesData.SaveTransactionResults(_orderId, _response.Authorization, _response.AuthorizationCode,
                                            _response.PrimaryReturnCodeString, _response.SecondaryReturnCode, _response.HashValue, _response.BatchID,
                                            _response.TransactionTimestamp, _response.OrderId, _response.RawHtml, ePayPayment.CreditCardInfo.CardType,
                                            Convert.ToInt32(status), failureReason);


            switch (_response.ResponseType)
            {
                case ResponseType.Approved:
                    return true;
                case ResponseType.Denied:
                    return false;
                default:
                    throw new Exception("ePayment Failed: " + failureReason);
            }
        }

        public void GenerateNewTraceNumber()
        {
            _products[0].GenerateNewTraceNumber();
            _traceNumber = _products[0].GetTraceNumber();
        }

    }
}
