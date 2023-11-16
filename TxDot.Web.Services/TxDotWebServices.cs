using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using TxDot.Web.Services.RtsAdmService;
using TxDot.Web.Services.RtsInvService;
using TxDot.Web.Services.RtsTransService;

namespace TxDot.Web.Services
{
    public class TxDotWebServices
    {
        private static string _caller = ConfigurationManager.AppSettings["TxDotWSCaller"];

        public static CountyInfo GetSpecificCountyInfo(string sessionId, string requestedCounty)
        {
            RtsAdmServiceService ws = new RtsAdmServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsAdmService-URL"];
            RtsDefaultRequest[] reqArray = new RtsDefaultRequest[1];
            RtsOfficeIdResponse[] respArray = new RtsOfficeIdResponse[1];

            RtsDefaultRequest req = new RtsDefaultRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-CountyInfo"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);

            CountyInfo county = new CountyInfo();
            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "GetSpecificCountyInfoList()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.getCountyInfo(reqArray);

                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());

                foreach (RtsOfficeIdsData o in respArray[0].officeData)
                {
                    if (o.countyName.ToUpper() == requestedCounty.ToUpper())
                    {
                        county.Name = new System.Globalization.CultureInfo("en").TextInfo.ToTitleCase(o.countyName.ToLower());
                        county.Number = o.number;
                        county.Email = o.emailAddress;
                        county.MailingAddress.Street1 = o.mailingAddress.streetLine1;
                        county.MailingAddress.Street2 = o.mailingAddress.streetLine2;
                        county.MailingAddress.City = o.mailingAddress.city;
                        county.MailingAddress.State = o.mailingAddress.state;
                        county.MailingAddress.ZIP = o.mailingAddress.zip;
                        county.MailingAddress.ZIP4 = o.mailingAddress.zipP4;
                        county.PhysicalAddress.Street1 = o.physicalAddress.streetLine1;
                        county.PhysicalAddress.Street2 = o.physicalAddress.streetLine2;
                        county.PhysicalAddress.City = o.physicalAddress.city;
                        county.PhysicalAddress.State = o.physicalAddress.state;
                        county.PhysicalAddress.ZIP = o.physicalAddress.zip;
                        county.PhysicalAddress.ZIP4 = o.physicalAddress.zipP4;

                        county.Phone = o.phoneNumber;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }
            return county;
        }

        public static List<string> GetCountyInfoList(string sessionId)
        {
            List<string> counties = new List<string>();

            RtsAdmServiceService ws = new RtsAdmServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsAdmService-URL"];
            RtsDefaultRequest[] reqArray = new RtsDefaultRequest[1];
            RtsOfficeIdResponse[] respArray = new RtsOfficeIdResponse[1];

            RtsDefaultRequest req = new RtsDefaultRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-CountyInfo"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);

            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();
            
            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "GetCountyInfoList()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.getCountyInfo(reqArray);
                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());

                foreach (RtsOfficeIdsData o in respArray[0].officeData)
                {
                    counties.Add(new System.Globalization.CultureInfo("en").TextInfo.ToTitleCase(o.countyName.ToLower()));
                }

                counties.Sort();
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }

            return counties;
        }

        public static List<CountyInfo> GetAllCountyMailingInfo(string sessionId)
        {
            List<CountyInfo> counties = new List<CountyInfo>();

            RtsAdmServiceService ws = new RtsAdmServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsAdmService-URL"];
            RtsDefaultRequest[] reqArray = new RtsDefaultRequest[1];
            RtsOfficeIdResponse[] respArray = new RtsOfficeIdResponse[1];

            RtsDefaultRequest req = new RtsDefaultRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-CountyInfo"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);

            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "GetCountyInfoList()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.getCountyInfo(reqArray);
                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());

                foreach (RtsOfficeIdsData o in respArray[0].officeData)
                {
                    CountyInfo cInfo = new CountyInfo();
                    cInfo.Name = o.countyName;
                    cInfo.MailingAddress.Street1 = o.mailingAddress.streetLine1;
                    cInfo.MailingAddress.Street2 = o.mailingAddress.streetLine2;
                    cInfo.MailingAddress.City = o.mailingAddress.city;
                    cInfo.MailingAddress.State = o.mailingAddress.state;
                    cInfo.MailingAddress.ZIP = o.mailingAddress.zip;
                    cInfo.MailingAddress.ZIP4 = o.mailingAddress.zipP4;
                    cInfo.Phone = o.phoneNumber;
                    cInfo.Email = o.emailAddress;
                    cInfo.TACName = o.tacName;

                    counties.Add(cInfo);
                }

            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }

            return counties;
        }

        public static TxDotResponse CheckPlateAvailability(string sessionId, string username, string plateText, string mfgText, 
                                                            string plateCode, bool fromReserve, bool isa)
        {
            RtsInvServiceService ws = new RtsInvServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsInvService-URL"];
            
            RtsInvRequest[] reqArray = new RtsInvRequest[1];
            RtsInvResponse[] respArray = new RtsInvResponse[1];

            RtsInvRequest req = new RtsInvRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-CheckAvailability"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);
            req.itmNo = plateText.ToUpper();
            req.manufacturingPltNo = mfgText;
            req.itmCd = plateCode;
            req.itmYr = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-ItmYr"]);
            req.requestingOfcIssuanceNo = Convert.ToInt32(ConfigurationManager.AppSettings["RequestingOfficeIssuanceNo"]);
            req.fromReserveFlag = fromReserve;
            req.isaFlg = isa;
            req.plpFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["TxDotWS-PLP-Flag"]);

            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());
            dict.Add("itmNo", req.itmNo.ToString());
            dict.Add("manufacturingPltNo", req.manufacturingPltNo.ToString());
            dict.Add("itmCd", req.itmCd.ToString());
            dict.Add("itmYr", req.itmYr.ToString());
            dict.Add("requestingOfcIssuanceNo", req.requestingOfcIssuanceNo.ToString());
            dict.Add("fromReserveFlag", req.fromReserveFlag.ToString());
            dict.Add("isaFlg", req.isaFlg.ToString());
            dict.Add("plpFlag", req.plpFlag.ToString());
            dict.Add("username", username);

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "CheckPlateAvailability()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.processData(reqArray);
                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }

            return new TxDotResponse(respArray[0].errMsgNo, respArray[0].errMsgDesc);
        }

        public static TxDotResponse HoldPlate(string sessionId, string username, string plateText, string mfgText, string plateCode, bool isa)
        {
            RtsInvServiceService ws = new RtsInvServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsInvService-URL"];
            RtsInvRequest[] reqArray = new RtsInvRequest[1];
            RtsInvResponse[] respArray = new RtsInvResponse[1];

            RtsInvRequest req = new RtsInvRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-HoldPlate"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);
            req.itmCd = plateCode;
            req.itmNo = plateText.ToUpper();
            req.itmYr = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-ItmYr"]);
            req.requestingOfcIssuanceNo = Convert.ToInt32(ConfigurationManager.AppSettings["RequestingOfficeIssuanceNo"]);
            req.fromReserveFlag = false;
            req.isaFlg = isa;
            req.plpFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["TxDotWS-PLP-Flag"]);
            req.manufacturingPltNo = mfgText;

            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());
            dict.Add("itmCd", req.itmCd.ToString());
            dict.Add("itmNo", req.itmNo.ToString());
            dict.Add("itmYr", req.itmYr.ToString());
            dict.Add("requestingOfcIssuanceNo", req.requestingOfcIssuanceNo.ToString());
            dict.Add("fromReserveFlag", req.fromReserveFlag.ToString());
            dict.Add("isaFlg", req.isaFlg.ToString());
            dict.Add("plpFlag", req.plpFlag.ToString());
            dict.Add("username", username);
            dict.Add("manufacturingPltNo", req.manufacturingPltNo);

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "HoldPlate()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.processData(reqArray);
                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }

            return new TxDotResponse(respArray[0].errMsgNo, respArray[0].errMsgDesc);
        }

        public static TxDotResponse HoldReservedPlate(string sessionId, string username, string plateText, string mfgText, string plateCode, bool isa)
        {
            RtsInvServiceService ws = new RtsInvServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsInvService-URL"];
            RtsInvRequest[] reqArray = new RtsInvRequest[1];
            RtsInvResponse[] respArray = new RtsInvResponse[1];

            RtsInvRequest req = new RtsInvRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-HoldReservedPlate"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);
            req.itmCd = plateCode;
            req.itmNo = plateText.ToUpper();
            req.manufacturingPltNo = mfgText;
            req.itmYr = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-ItmYr"]);
            req.requestingOfcIssuanceNo = Convert.ToInt32(ConfigurationManager.AppSettings["RequestingOfficeIssuanceNo"]);
            req.fromReserveFlag = true;
            req.isaFlg = isa;
            req.plpFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["TxDotWS-PLP-Flag"]);

            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());
            dict.Add("itmCd", req.itmCd.ToString());
            dict.Add("itmNo", req.itmNo.ToString());
            dict.Add("itmYr", req.itmYr.ToString());
            dict.Add("requestingOfcIssuanceNo", req.requestingOfcIssuanceNo.ToString());
            dict.Add("fromReserveFlag", req.fromReserveFlag.ToString());
            dict.Add("isaFlg", req.isaFlg.ToString());
            dict.Add("plpFlag", req.plpFlag.ToString());
            dict.Add("username", username);
            dict.Add("manufacturingPltNo", req.manufacturingPltNo);

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "HoldReservedPlate()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.processData(reqArray);
                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }

            return new TxDotResponse(respArray[0].errMsgNo, respArray[0].errMsgDesc);
        }

        public static TxDotResponse RenewHold(string sessionId, string username, string plateText, string mfgText, string plateCode, bool fromReserve, bool isa)
        {
            RtsInvServiceService ws = new RtsInvServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsInvService-URL"];
            RtsInvRequest[] reqArray = new RtsInvRequest[1];
            RtsInvResponse[] respArray = new RtsInvResponse[1];

            RtsInvRequest req = new RtsInvRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-RenewHold"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);
            req.itmCd = plateCode;
            req.itmNo = plateText.ToUpper();
            req.manufacturingPltNo = mfgText;
            req.itmYr = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-ItmYr"]);
            req.requestingOfcIssuanceNo = Convert.ToInt32(ConfigurationManager.AppSettings["RequestingOfficeIssuanceNo"]);
            req.fromReserveFlag = fromReserve;
            req.isaFlg = isa;
            req.plpFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["TxDotWS-PLP-Flag"]);

            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());
            dict.Add("itmCd", req.itmCd.ToString());
            dict.Add("itmNo", req.itmNo.ToString());
            dict.Add("itmYr", req.itmYr.ToString());
            dict.Add("requestingOfcIssuanceNo", req.requestingOfcIssuanceNo.ToString());
            dict.Add("fromReserveFlag", req.fromReserveFlag.ToString());
            dict.Add("isaFlg", req.isaFlg.ToString());
            dict.Add("plpFlag", req.plpFlag.ToString());
            dict.Add("username", username);
            dict.Add("manufacturingPltNo", req.manufacturingPltNo);

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "RenewHold()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.processData(reqArray);
                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }

            return new TxDotResponse(respArray[0].errMsgNo, respArray[0].errMsgDesc);
        }

        public static TxDotResponse CancelHold(string sessionId, string username, string plateText, string mfgText, string plateCode, bool fromReserve, bool isa)
        {
            RtsInvServiceService ws = new RtsInvServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsInvService-URL"];
            RtsInvRequest[] reqArray = new RtsInvRequest[1];
            RtsInvResponse[] respArray = new RtsInvResponse[1];

            RtsInvRequest req = new RtsInvRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-CancelHold"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);
            req.itmCd = plateCode;
            req.itmNo = plateText.ToUpper();
            req.manufacturingPltNo = mfgText;
            req.itmYr = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-ItmYr"]);
            req.requestingOfcIssuanceNo = Convert.ToInt32(ConfigurationManager.AppSettings["RequestingOfficeIssuanceNo"]);
            req.fromReserveFlag = fromReserve;
            req.isaFlg = isa;
            req.plpFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["TxDotWS-PLP-Flag"]);
            
            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());
            dict.Add("itmCd", req.itmCd.ToString());
            dict.Add("itmNo", req.itmNo.ToString());
            dict.Add("itmYr", req.itmYr.ToString());
            dict.Add("requestingOfcIssuanceNo", req.requestingOfcIssuanceNo.ToString());
            dict.Add("fromReserveFlag", req.fromReserveFlag.ToString());
            dict.Add("isaFlg", req.isaFlg.ToString());
            dict.Add("plpFlag", req.plpFlag.ToString());
            dict.Add("username", username);
            dict.Add("manufacturingPltNo", req.manufacturingPltNo);

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "CancelHold()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.processData(reqArray);

                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }

            return new TxDotResponse(respArray[0].errMsgNo, respArray[0].errMsgDesc);
        }

        public static TxDotResponse OrderPlate(string sessionId, string username, string plateCode, string plateText, string mfgText, string street1, string street2, 
                string city, string state, string zip, string zip4, int countyNum, string firstName, string lastName, string phone, string email, int renewalPeriod, 
                decimal paymentAmount, DateTime requestTime, string traceNumber, int orderId, DateTime ePaySendTime, DateTime ePayReceiveTime)
        {
            RtsTransServiceService ws = new RtsTransServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsTransService-URL"];
            
            RtsTransRequest[] reqArray = new RtsTransRequest[1];
            RtsTransResponse[] respArray = new RtsTransResponse[1];

            RtsTransRequest req = new RtsTransRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-OrderPlate"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);
            req.pltCd = plateCode;
            req.pltNo = plateText;
            req.mfgPltNo = mfgText;
            
            int expMonth = DateTime.Now.Month - 1;
            if (expMonth == 0)
            {
                expMonth = 12;
            }

            req.pltExpMo = expMonth;
            req.pltExpYr = DateTime.Now.Year + renewalPeriod;
            //prev month + 1 year

            TxDot.Web.Services.RtsTransService.RtsAddress address = new TxDot.Web.Services.RtsTransService.RtsAddress();
            address.streetLine1 = street1;
            address.streetLine2 = street2;
            address.city = city;
            address.state = state;
            address.zip = zip;
            address.zipP4 = zip4;


            req.pltOwnrAddr = address;
            req.pltOwnrName1 = firstName + " " + lastName;
            req.pltOwnrName2 = string.Empty;
            req.pltOwnrPhone = phone;
            req.pltOwnrEmailAddr = email;
            req.pltTerm = renewalPeriod;
            req.pymntAmt = Convert.ToDouble(paymentAmount);
            req.initReqTimeStmp = requestTime;
            
            req.epaySendTimeStmp = ePaySendTime;
            req.epayRcveTimeStmp = ePayReceiveTime;
            req.itrntPymntStatusCd = 0;
            req.pymntOrderId = orderId.ToString();
            
            req.isa = false;
            req.plp = true;
            req.fromReserve = false;
            req.resComptCntyNo = countyNum;
            req.orgNo = ConfigurationManager.AppSettings["TxDotWS-OrgNo"];
            req.itrntTraceNo = traceNumber;

            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());
            dict.Add("pltCd", req.pltCd.ToString());
            dict.Add("pltNo", req.pltNo.ToString());
            dict.Add("mfgPltNo", req.mfgPltNo.ToString());
            dict.Add("streetLine1", req.pltOwnrAddr.streetLine1.ToString());
            dict.Add("streetLine2", req.pltOwnrAddr.streetLine2.ToString());
            dict.Add("city", req.pltOwnrAddr.city.ToString());
            dict.Add("state", req.pltOwnrAddr.state.ToString());
            dict.Add("zip", req.pltOwnrAddr.zip.ToString());
            dict.Add("zip4", req.pltOwnrAddr.zipP4.ToString());
            dict.Add("pltOwnrName1", req.pltOwnrName1.ToString());
            dict.Add("pltOwnrName2", req.pltOwnrName2.ToString());
            dict.Add("pltOwnrPhone", req.pltOwnrPhone.ToString());
            dict.Add("pltOwnrEmailAddr", req.pltOwnrEmailAddr.ToString());
            dict.Add("pltTerm", req.pltTerm.ToString());
            dict.Add("pymntAmt", req.pymntAmt.ToString());
            dict.Add("initReqTimeStmp", req.initReqTimeStmp.ToString());
            dict.Add("epaySendTimeStmp", req.epaySendTimeStmp.ToString());
            dict.Add("epayRcveTimeStmp", req.epayRcveTimeStmp.ToString());
            dict.Add("itrntPymntStatusCd", req.itrntPymntStatusCd.ToString());
            dict.Add("pymntOrderId", req.pymntOrderId.ToString());
            dict.Add("isa", req.isa.ToString());
            dict.Add("plp", req.plp.ToString());
            dict.Add("fromReserve", req.fromReserve.ToString());
            dict.Add("resComptCntyNo", req.resComptCntyNo.ToString());
            dict.Add("orgNo", req.orgNo.ToString());
            dict.Add("itrntTraceNo", req.itrntTraceNo.ToString());
            dict.Add("pltExpMo", req.pltExpMo.ToString());
            dict.Add("pltExpYr", req.pltExpYr.ToString());
            dict.Add("username", username);

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "OrderPlate()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.processData(reqArray);

                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }
            
            return new TxDotResponse(respArray[0].errMsgNo, respArray[0].errMsgDesc);
        }

        public static TxDotResponse OrderReservedPlate(string sessionId, string username, string plateCode, string plateText, string mfgText, string street1, 
                string street2, string city, string state, string zip, string zip4, int countyNum, string firstName, string lastName, string phone, string email, int renewalPeriod,
                decimal paymentAmount, DateTime requestTime, string traceNumber, int orderId, DateTime ePaySendTime, DateTime ePayReceiveTime, bool isa)
        {
            RtsTransServiceService ws = new RtsTransServiceService();
            ws.Url = ConfigurationManager.AppSettings["TxDotWS-RtsTransService-URL"];
            RtsTransRequest[] reqArray = new RtsTransRequest[1];
            RtsTransResponse[] respArray = new RtsTransResponse[1];

            RtsTransRequest req = new RtsTransRequest();
            req.action = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWSAction-OrderReservedPlate"]);
            req.caller = _caller;
            req.sessionId = sessionId;
            req.versionNo = Convert.ToInt32(ConfigurationManager.AppSettings["TxDotWS-VersionNo"]);
            req.pltCd = plateCode;
            req.pltNo = plateText;
            req.mfgPltNo = mfgText;

            TxDot.Web.Services.RtsTransService.RtsAddress address = new TxDot.Web.Services.RtsTransService.RtsAddress();
            address.streetLine1 = street1;
            address.streetLine2 = street2;
            address.city = city;
            address.state = state;
            address.zip = zip;
            address.zipP4 = zip4;

            req.pltOwnrAddr = address;
            req.pltOwnrName1 = firstName + " " + lastName;
            req.pltOwnrName2 = string.Empty;
            req.pltOwnrPhone = phone;
            req.pltOwnrEmailAddr = email;
            req.pltTerm = renewalPeriod;
            req.pymntAmt = Convert.ToDouble(paymentAmount);
            req.initReqTimeStmp = requestTime;

            req.epaySendTimeStmp = ePaySendTime;
            req.epayRcveTimeStmp = ePayReceiveTime;
            req.itrntPymntStatusCd = 0;
            req.pymntOrderId = orderId.ToString();

            req.isa = isa;
            req.plp = true;
            req.fromReserve = true;
            req.resComptCntyNo = countyNum;
            req.orgNo = ConfigurationManager.AppSettings["TxDotWS-OrgNo"];
            req.itrntTraceNo = traceNumber;

            int expMonth = DateTime.Now.Month - 1;
            if (expMonth == 0)
            {
                expMonth = 12;
            }

            req.pltExpMo = expMonth;
            req.pltExpYr = DateTime.Now.Year + renewalPeriod;

            reqArray[0] = req;

            IDictionary<string, object> dict = new Dictionary<string, object>();
            LogEntry wsLog = new LogEntry();

            dict.Add("URL", ws.Url.ToString());
            dict.Add("sessionId", req.sessionId);
            dict.Add("action", req.action.ToString());
            dict.Add("caller", req.caller.ToString());
            dict.Add("versionNo", req.versionNo.ToString());
            dict.Add("pltCd", req.pltCd.ToString());
            dict.Add("pltNo", req.pltNo.ToString());
            dict.Add("mfgPltNo", req.mfgPltNo.ToString());
            dict.Add("streetLine1", req.pltOwnrAddr.streetLine1.ToString());
            dict.Add("streetLine2", req.pltOwnrAddr.streetLine2.ToString());
            dict.Add("city", req.pltOwnrAddr.city.ToString());
            dict.Add("state", req.pltOwnrAddr.state.ToString());
            dict.Add("zip", req.pltOwnrAddr.zip.ToString());
            dict.Add("zip4", req.pltOwnrAddr.zipP4.ToString());
            dict.Add("pltOwnrName1", req.pltOwnrName1.ToString());
            dict.Add("pltOwnrName2", req.pltOwnrName2.ToString());
            dict.Add("pltOwnrPhone", req.pltOwnrPhone.ToString());
            dict.Add("pltOwnrEmailAddr", req.pltOwnrEmailAddr.ToString());
            dict.Add("pltTerm", req.pltTerm.ToString());
            dict.Add("pymntAmt", req.pymntAmt.ToString());
            dict.Add("initReqTimeStmp", req.initReqTimeStmp.ToString());
            dict.Add("epaySendTimeStmp", req.epaySendTimeStmp.ToString());
            dict.Add("epayRcveTimeStmp", req.epayRcveTimeStmp.ToString());
            dict.Add("itrntPymntStatusCd", req.itrntPymntStatusCd.ToString());
            dict.Add("pymntOrderId", req.pymntOrderId.ToString());
            dict.Add("isa", req.isa.ToString());
            dict.Add("plp", req.plp.ToString());
            dict.Add("fromReserve", req.fromReserve.ToString());
            dict.Add("resComptCntyNo", req.resComptCntyNo.ToString());
            dict.Add("orgNo", req.orgNo.ToString());
            dict.Add("itrntTraceNo", req.itrntTraceNo.ToString());
            dict.Add("pltExpMo", req.pltExpMo.ToString());
            dict.Add("pltExpYr", req.pltExpYr.ToString());
            dict.Add("username", username);

            wsLog.Title = "TxDot WebService";
            wsLog.Message = "OrderReservedPlate()";
            wsLog.TimeStamp = DateTime.UtcNow;
            wsLog.Categories.Add("WebService Call");

            try
            {
                respArray = ws.processData(reqArray);

                dict.Add("errMsgNo", respArray[0].errMsgNo.ToString());
                dict.Add("errMsgDesc", respArray[0].errMsgDesc == null ? string.Empty : respArray[0].errMsgDesc.ToString());
            }
            catch (Exception exc)
            {
                dict.Add("errMsgNo", ConfigurationManager.AppSettings["DefaultWebServiceErrorCode"]);
                dict.Add("errMsgDesc", exc.Message);
                wsLog.Categories.Add("WebService Error");
                throw;
            }
            finally
            {
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);
            }

            return new TxDotResponse(respArray[0].errMsgNo, respArray[0].errMsgDesc);
        }
    }

}