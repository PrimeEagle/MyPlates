using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MyPlates.Tx.Data
{
    public class MyPlatesData
    {
        public static DataSet GetFlashXMLInfo()
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase();
            
            DbCommand cmd = db.GetStoredProcCommand("GetCategoryDescriptions");
            db.LoadDataSet(cmd, ds, "TableCat");

            cmd = db.GetStoredProcCommand("GetPlateCodeInfo");
            db.LoadDataSet(cmd, ds, "TablePlate");

            cmd = db.GetStoredProcCommand("GetSections");
            db.LoadDataSet(cmd, ds, "TableSection");

            return ds;
        }

        public static DataSet GetOrderInfo(int orderId)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("GetOrderInfo");
            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId);

            return db.ExecuteDataSet(cmd);
        }

        public static DataSet GetPlateGuids(DateTime startDate, DateTime endDate)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("GetPlateGuids");
            db.AddInParameter(cmd, "@startDate", DbType.DateTime, startDate == DateTime.MinValue ? (object)DBNull.Value : (object)startDate);
            db.AddInParameter(cmd, "@endDate", DbType.DateTime, endDate == DateTime.MinValue ? (object)DBNull.Value : (object)endDate);

            return db.ExecuteDataSet(cmd);
        }

        public static List<string> GetPlateCodeValidPatterns(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPlateCodePatterns");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<string> patterns = new List<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                patterns.Add(row[0].ToString());
            }

            return patterns;
        }

        public static int GetPlateCategoryId(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPlateCategoryId");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }

        public static int GetOrderIDForPlateText(string plateText)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetOrderIDForPlateText");

            db.AddInParameter(cmd, "@plateText", DbType.String, plateText);
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }

        public static string GetPlateCategoryName(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPlateCategoryName");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);
            return db.ExecuteScalar(cmd).ToString();
        }

        public static string GetPlateTypeName(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPlateTypeName");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);
            return db.ExecuteScalar(cmd).ToString();
        }

        public static string GetRegExForPlateCode(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetRegExForPlateCode");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);
            return db.ExecuteScalar(cmd).ToString();
        }

        public static string GetRegExISAForPlateCode(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetRegExISAForPlateCode");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);
            return db.ExecuteScalar(cmd).ToString();
        }

        // returns Customer_ID
        public static int CreateCustomer(string firstName, string lastName, string street1, string street2, string city, string state, string county,
                                            string zip, string zip4, string email, string phone)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("CreateCustomer");

            db.AddInParameter(cmd, "@firstName", DbType.String, firstName);
            db.AddInParameter(cmd, "@lastName", DbType.String, lastName);
            db.AddInParameter(cmd, "@street1", DbType.String, street1);
            db.AddInParameter(cmd, "@street2", DbType.String, street2);
            db.AddInParameter(cmd, "@city", DbType.String, city);
            db.AddInParameter(cmd, "@state", DbType.String, state);
            db.AddInParameter(cmd, "@county", DbType.String, county);

            string zipString = zip;
            if (zip4.Length > 0)
            {
                zipString = zipString + "-" + zip4;
            }
            db.AddInParameter(cmd, "@zip", DbType.String, zipString);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@phone", DbType.String, phone);
            db.AddOutParameter(cmd, "@customerid", DbType.Int32, 10);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@customerid"));
        }

        public static void RecordPlateAvailabilityCheck(string ip, string username, string plateText, bool available)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("RecordPlateAvailabilityCheck");

            db.AddInParameter(cmd, "@ip", DbType.String, ip);
            db.AddInParameter(cmd, "@username", DbType.String, username);
            db.AddInParameter(cmd, "@plateText", DbType.String, plateText);
            db.AddInParameter(cmd, "@available", DbType.Boolean, available);

            db.ExecuteNonQuery(cmd);
        }

        public static void SaveOrderReceipt(int orderId, byte[] receipt)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("SaveOrderReceipt");

            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId);
            db.AddInParameter(cmd, "@receipt", DbType.Binary, receipt);

            db.ExecuteNonQuery(cmd);
        }

        public static byte[] GetOrderReceipt(int orderId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetOrderReceipt");

            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId);
            
            object receipt = db.ExecuteScalar(cmd);
            if (receipt == null || receipt == DBNull.Value)
            {
                return null;
            }
            else
            {
                return (byte[])receipt;
            }
        }

        // returns Owner_ID
        public static int CreateOwner(string firstName, string lastName, string street1, string street2, string city, string state, string county,
                                            string zip, string zip4, string email, string phone)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("CreateOwner");

            db.AddInParameter(cmd, "@firstName", DbType.String, firstName);
            db.AddInParameter(cmd, "@lastName", DbType.String, lastName);
            db.AddInParameter(cmd, "@street1", DbType.String, street1);
            db.AddInParameter(cmd, "@street2", DbType.String, street2);
            db.AddInParameter(cmd, "@city", DbType.String, city);
            db.AddInParameter(cmd, "@state", DbType.String, state);
            db.AddInParameter(cmd, "@county", DbType.String, county);
            string zipString = zip;
            if (zip4.Length > 0)
            {
                zipString = zipString + "-" + zip4;
            }
            db.AddInParameter(cmd, "@zip", DbType.String, zipString);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@phone", DbType.String, phone);
            db.AddOutParameter(cmd, "@ownerid", DbType.Int32, 10);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@ownerid"));
        }

        // returns Order_ID
        public static int CreateOrder(string sessionId, int customerId, string username, string ip, string traceNumber)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("CreateOrder");

            db.AddInParameter(cmd, "@customerId", DbType.Int32, customerId);
            db.AddInParameter(cmd, "@sessionId", DbType.String, sessionId);
            db.AddInParameter(cmd, "@username", DbType.String, username);
            db.AddInParameter(cmd, "@ip", DbType.String, ip);
            db.AddInParameter(cmd, "@traceNum", DbType.String, traceNumber);
            db.AddOutParameter(cmd, "@orderId", DbType.Int32, 0);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd,"@orderId"));
        }

        public static void SaveTransactionResults(int orderId, string authorization, string authCode, string primaryReturnCode, string secondaryReturnCode,
                                                string hashValue, string batchId, DateTime transactionTimestamp, string ePayOrderId, string ePayResponse,
                                                string paymentMethod, int status, string failureReason)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("SaveTransactionResults");

            db.AddInParameter(cmd, "@authorizationNumber", DbType.String, authorization);
            db.AddInParameter(cmd, "@authCode", DbType.String, authCode);
            db.AddInParameter(cmd, "@primaryReturnCode", DbType.String, primaryReturnCode);
            db.AddInParameter(cmd, "@secondaryReturnCode", DbType.String, secondaryReturnCode);
            db.AddInParameter(cmd, "@hashValue", DbType.String, hashValue);
            db.AddInParameter(cmd, "@ePayResponse", DbType.String, ePayResponse);
            db.AddInParameter(cmd, "@batchId", DbType.String, batchId);
            db.AddInParameter(cmd, "@transactionTimestamp", DbType.DateTime, transactionTimestamp);
            db.AddInParameter(cmd, "@ePayOrderId", DbType.String, ePayOrderId);
            db.AddInParameter(cmd, "@paymentMethod", DbType.String, paymentMethod);
            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId);
            db.AddInParameter(cmd, "@status", DbType.Int32, status);
            if (failureReason == string.Empty)
            {
                db.AddInParameter(cmd, "@failureReason", DbType.String, System.DBNull.Value);
            }
            else
            {
                db.AddInParameter(cmd, "@failureReason", DbType.String, failureReason);
            }

            db.ExecuteNonQuery(cmd);
        }

        public static void RecordResevedPlateOrder(string plateText)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("RecordResevedPlateOrder");

            db.AddInParameter(cmd, "@plateText", DbType.String, plateText);
            db.ExecuteNonQuery(cmd);
        }

        public static void SaveTxDotOrderResult(int orderId, int status, DateTime orderTime, string failureReason)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("SaveTxDotOrderResult");

            if (failureReason == string.Empty)
            {
                db.AddInParameter(cmd, "@failureReason", DbType.String, System.DBNull.Value);
            }
            else
            {
                db.AddInParameter(cmd, "@failureReason", DbType.String, failureReason);
            }
            db.AddInParameter(cmd, "@orderTime", DbType.DateTime, orderTime);
            db.AddInParameter(cmd, "@status", DbType.Int32, status);
            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId);

            db.ExecuteNonQuery(cmd);
        }

        public static bool GetReservedPlateAvailability(string plateText)
        {
            Database db = DatabaseFactory.CreateDatabase();
            
            DbCommand cmd = db.GetStoredProcCommand("GetReservedPlateAvailability");
            
            db.AddInParameter(cmd, "@plateText", DbType.String, plateText);

            return Convert.ToBoolean(db.ExecuteScalar(cmd));
        }

        public static bool GetReservedPlateStatus(string plateText)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("GetReservedPlateStatus");

            db.AddInParameter(cmd, "@plateText", DbType.String, plateText);
            db.AddOutParameter(cmd, "@reserved", DbType.Boolean, 1);

            db.ExecuteNonQuery(cmd);

            return Convert.ToBoolean(db.GetParameterValue(cmd, "@reserved"));
        }

        public static void CreateOrderPlate(int orderId, int ownerId, string plateCode, string plateText, string mfgText, Guid plateID, byte[] plateImage,
                                int renewalPeriod, decimal costTotal, decimal costPlateFee, decimal costRevenuePercentage,
                                string countyStreet1, string countyStreet2, string countyCity, string countyState, string countyZIP,
                                string countyPhone, string countyEmail)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("CreateOrderPlate");

            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId);
            db.AddInParameter(cmd, "@ownerId", DbType.Int32, ownerId);
            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);
            db.AddInParameter(cmd, "@plateText", DbType.String, plateText);
            db.AddInParameter(cmd, "@mfgText", DbType.String, mfgText);
            db.AddInParameter(cmd, "@renewalPeriod", DbType.Int32, renewalPeriod);
            db.AddInParameter(cmd, "@costTotal", DbType.Decimal, costTotal);
            db.AddInParameter(cmd, "@costPlateFee", DbType.Decimal, costPlateFee);
            db.AddInParameter(cmd, "@costRevenuePercentage", DbType.Decimal, costRevenuePercentage);
            db.AddInParameter(cmd, "@plateImage", DbType.Binary, plateImage);
            db.AddInParameter(cmd, "@plateID", DbType.Guid, plateID);
            db.AddInParameter(cmd, "@countyStreet1", DbType.String, countyStreet1);
            db.AddInParameter(cmd, "@countyStreet2", DbType.String, (countyStreet2.Length == 0 || countyStreet2 == null) ? (object)DBNull.Value : (object)countyStreet2);
            db.AddInParameter(cmd, "@countyCity", DbType.String, countyCity);
            db.AddInParameter(cmd, "@countyState", DbType.String, countyState);
            db.AddInParameter(cmd, "@countyZIP", DbType.String, countyZIP);
            db.AddInParameter(cmd, "@countyPhone", DbType.String, countyPhone);
            db.AddInParameter(cmd, "@countyEmail", DbType.String, countyEmail);

            db.ExecuteNonQuery(cmd);
        }

        public static DataSet GetCategories()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetCategories");

            return db.ExecuteDataSet(cmd);
        }

        public static DataSet GetCustomerForOrderId(int orderId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetCustomerForOrderId");
            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId);

            return db.ExecuteDataSet(cmd);
        }

        public static DataSet GetPlateInfoForOrderId(int orderId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPlateInfoForOrderId");
            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId);

            return db.ExecuteDataSet(cmd);
        }

        public static DataSet SearchCustomersOwners(string lastName, string firstName, string email, string phone, string zip, string county, string username,
                                                    DateTime fromDate, DateTime toDate, int orderId, string plateText)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("SearchCustomersOwners");
            db.AddInParameter(cmd, "@lastName", DbType.String, lastName == string.Empty ? (object)DBNull.Value : (object)lastName);
            db.AddInParameter(cmd, "@firstName", DbType.String, firstName == string.Empty ? (object)DBNull.Value : (object)firstName);
            db.AddInParameter(cmd, "@email", DbType.String, email == string.Empty ? (object)DBNull.Value : (object)email);
            db.AddInParameter(cmd, "@phone", DbType.String, phone == string.Empty ? (object)DBNull.Value : (object)phone);
            db.AddInParameter(cmd, "@zip", DbType.String, zip == string.Empty ? (object)DBNull.Value : (object)zip);
            db.AddInParameter(cmd, "@county", DbType.String, county == string.Empty ? (object)DBNull.Value : (object)county);
            db.AddInParameter(cmd, "@username", DbType.String, username == string.Empty ? (object)DBNull.Value : (object)username);
            db.AddInParameter(cmd, "@fromDate", DbType.DateTime, fromDate <= DateTime.MinValue ? (object)DBNull.Value : (object)fromDate);
            db.AddInParameter(cmd, "@toDate", DbType.DateTime, toDate <= DateTime.MinValue ? (object)DBNull.Value : (object)toDate);
            db.AddInParameter(cmd, "@orderId", DbType.Int32, orderId <= 0 ? (object)DBNull.Value : (object)orderId);
            db.AddInParameter(cmd, "@plateText", DbType.String, plateText == string.Empty ? (object)DBNull.Value : (object)plateText);
            return db.ExecuteDataSet(cmd);
        }

        public static DataSet GetAvailableISAPlates()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetAvailableISAPlates");

            return db.ExecuteDataSet(cmd);
        }

        public static byte[] GetPlateImage(Guid plateId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPlateImage");
            db.AddInParameter(cmd, "@plateId", DbType.Guid, plateId);

            object image = db.ExecuteScalar(cmd);
            if (image == null || image == DBNull.Value)
            {
                return null;
            }
            else
            {
                return (byte[])image;
            }
        }

        public static DataSet GetSections()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetSections");

            return db.ExecuteDataSet(cmd);
        }

        public static DataSet GetPlateCodes(int categoryId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("GetPlateCodesByCategory");
            db.AddInParameter(cmd, "@categoryId",DbType.Int32, categoryId);
            
            return db.ExecuteDataSet(cmd);
        }

        public static DataSet GetPlateCodes()
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("GetPlateCodes");

            return db.ExecuteDataSet(cmd);
        }

        public static DataSet GetRenewalPeriods()
        {
            Database db = DatabaseFactory.CreateDatabase();

            return db.ExecuteDataSet("GetRenewalPeriods");
        }

        public static decimal GetTotalCost(int categoryId, int renewalPeriod)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("GetTotalCost");
            db.AddInParameter(cmd, "@categoryId", DbType.Int32, categoryId);
            db.AddInParameter(cmd, "@renewalPeriod", DbType.Int32, renewalPeriod);

            return (decimal)db.ExecuteScalar(cmd);
        }

        public static decimal GetYearlyPlateFee (int categoryId, int renewalPeriod)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("GetYearlyPlateFee");
            db.AddInParameter(cmd, "@categoryId", DbType.Int32, categoryId);
            db.AddInParameter(cmd, "@renewalPeriod", DbType.Int32, renewalPeriod);

            return (decimal)db.ExecuteScalar(cmd);
        }

        public static decimal GetGeneralRevenuePercentage(int categoryId, int renewalPeriod)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetGeneralRevenuePercentage");

            db.AddInParameter(cmd, "@categoryId", DbType.Int32, categoryId);
            db.AddInParameter(cmd, "@renewalPeriod", DbType.Int32, renewalPeriod);

            return Convert.ToDecimal(db.ExecuteScalar(cmd));
        }

        public static IDataReader GetPricingInformation(string plateCode, int renewalPeriod)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPricingByCategoryIdRenewalPeriod");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);
            db.AddInParameter(cmd, "@renewalPeriod", DbType.Int32, renewalPeriod);

            return db.ExecuteReader(cmd);
        }

        public static IDataReader GetPlateImageSize(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPlateImageSize");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);

            return db.ExecuteReader(cmd);
        }

        public static IDataReader GetPlateSuggestionInformation(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetPlateSuggestionInformation");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);

            return db.ExecuteReader(cmd);
        }

        public static DataSet GetCreditCardTypes()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetCreditCardTypes");

            return db.ExecuteDataSet(cmd);
        }

        public static int GetOrderId(string sessionId, string traceNumber)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetOrderID");

            db.AddInParameter(cmd, "@sessionId", DbType.String, sessionId);
            db.AddInParameter(cmd, "@traceNum", DbType.String, traceNumber);

            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }

        public static int GetCategoryForPlateCode(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetCategoryForPlateCode");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);

            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }

        public static string GetClassForPlateCode(string plateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetClassForPlateCode");

            db.AddInParameter(cmd, "@plateCode", DbType.String, plateCode);

            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        public static DataSet GetReservedSeriesSuggestions(string plateTextAlpha, int plateTextNum, int numSuggestions)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetReservedSeriesSuggestions");

            db.AddInParameter(cmd, "@plateTextAlpha", DbType.String, plateTextAlpha);
            db.AddInParameter(cmd, "@plateTextNum", DbType.Int32, plateTextNum);

            DataSet ds = db.ExecuteDataSet(cmd);

            for (int i = ds.Tables[0].Rows.Count - 1; i >= numSuggestions; i--)
            {
                ds.Tables[0].Rows.RemoveAt(i);
            }

            return ds;
        }

        public static DataSet GetStates()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetStates");

            return db.ExecuteDataSet(cmd);
        }

        public static DataSet GetMonths()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetMonths");

            return db.ExecuteDataSet(cmd);
        }

        public static string GetSuggestedSubstitute(string originalChar)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("GetSuggestedSubstitute");

            db.AddInParameter(cmd, "@originalChar", DbType.String, originalChar);

            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        public static DataSet GetUsernames()
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand cmd = db.GetStoredProcCommand("GetCSRList");
            return db.ExecuteDataSet(cmd);
        }

        public static string LogObjectionableText(string plateText, string username)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("LogObjectionableText");

            db.AddInParameter(cmd, "@plateText", DbType.String, plateText.ToUpper());
            db.AddInParameter(cmd, "@username", DbType.String, username);

            return Convert.ToString(db.ExecuteScalar(cmd));
        }
    }
}
