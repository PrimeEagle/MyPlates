using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MyPlates.Tx.Carts;
using MyPlates.Tx.Configuration;
using TxDot.Web.Services;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace MyPlates.Tx.Web
{
    public partial class UIAddPlateToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LicensePlate plate = new LicensePlate();
            string result = "";

            try
            {
                plate.PlateCode = Request.Form["PlateCode"] == null ? string.Empty : Request.Form["PlateCode"];
                plate.MfgText = Request.Form["PlateText"] == null ? string.Empty : Request.Form["PlateText"];
                plate.RenewalPeriod = Request.Form["RenewalPeriod"] == null ? 0 : Convert.ToInt32(Request.Form["RenewalPeriod"]);
                plate.OwnerInfo.NameFirst = Request.Form["FirstName"] == null ? string.Empty : Request.Form["FirstName"];
                plate.OwnerInfo.NameLast = Request.Form["LastName"] == null ? string.Empty : Request.Form["LastName"];
                plate.OwnerInfo.Street1 = Request.Form["Street1"] == null ? string.Empty : Request.Form["Street1"];
                plate.OwnerInfo.Street2 = Request.Form["Street2"] == null ? string.Empty : Request.Form["Street2"];
                plate.OwnerInfo.City = Request.Form["City"] == null ? string.Empty : Request.Form["City"];
                plate.OwnerInfo.County = Request.Form["County"] == null ? string.Empty : Request.Form["County"];

                string zip = string.Empty;
                string zip4 = string.Empty;

                string zipString = Request.Form["ZipCode"];

                if (zipString != null)
                {
                    if (zipString.Length == 5)
                    {
                        zip = zipString;
                    }

                    if (zipString.Length == 10)
                    {
                        zip = zipString.Substring(0, 5);
                        zip4 = zipString.Substring(6, 4);
                    }
                }

                plate.OwnerInfo.ZIP = zip;
                plate.OwnerInfo.ZIP4 = zip4;

                plate.OwnerInfo.Phone = Request.Form["Phone"] == null ? string.Empty : Request.Form["Phone"];
                plate.OwnerInfo.Email = Request.Form["Email"] == null ? string.Empty : Request.Form["Email"];

                Regex rx = new Regex(@"^\d{5}(-\d{4})?$");
                if (!rx.IsMatch(zipString))
                {
                    result = "error";
                }

                rx = new Regex(@"^\d{5}$");
                if (!rx.IsMatch(plate.OwnerInfo.ZIP))
                {
                    result = "error";
                }

                rx = new Regex(@"^(\d{4})?$");
                if (!rx.IsMatch(zip4))
                {
                    result = "error";
                }

                rx = new Regex(@"^(((\(\d{3}\)((-?)|( ?)))|(\d{3}-))\d{3}-\d{4})$|^\d{10}$");
                if (!rx.IsMatch(plate.OwnerInfo.Phone))
                {
                    result = "error";
                }

                rx = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!rx.IsMatch(plate.OwnerInfo.Email))
                {
                    result = "error";
                }

                rx = new Regex(@"(?=[A-Za-z])");
                if (!rx.IsMatch(plate.OwnerInfo.Street1))
                {
                    result = "error";
                }

                rx = new Regex(@"(?=[A-Za-z])");
                if (!rx.IsMatch(plate.OwnerInfo.Street2) && plate.OwnerInfo.Street2.Length > 0)
                {
                    result = "error";
                }


                if (plate.PlateCode == string.Empty ||
                    plate.MfgText == string.Empty ||
                    plate.RenewalPeriod == 0 ||
                    plate.OwnerInfo.NameFirst == string.Empty ||
                    plate.OwnerInfo.NameLast == string.Empty ||
                    plate.OwnerInfo.Street1 == string.Empty ||
                    plate.OwnerInfo.City == string.Empty ||
                    plate.OwnerInfo.County == string.Empty ||
                    plate.OwnerInfo.ZIP == string.Empty ||
                    plate.OwnerInfo.Phone == string.Empty ||
                    plate.OwnerInfo.Email == string.Empty
                    )
                {
                    result = "error";
                }

                if (result != "error")
                {
                    //CountyInfo county = TxDotWebServices.GetSpecificCountyInfo(Session.SessionID, plate.OwnerInfo.County);
                    //plate.OwnerInfo.CountyNumber = county.Number;

                    PlateConfiguration.LoadPlateData(plate);

                    bool success = false;

                    bool available = false;

                    available = PlateConfiguration.CheckPlateAvailability(plate.MfgText, plate.PlateCode);

                    ShoppingCart cart = SessionManagement.RetrieveShoppingCart();

                    foreach (LicensePlate p in cart.Plates)
                    {
                        if (p.Text.ToUpper() == plate.Text.ToUpper())
                        {
                            available = false;
                        }
                    }

                    if (available)
                    {
                        success = PlateConfiguration.HoldPlate(plate.MfgText, plate.PlateCode);
                    }

                    if (success)
                    {
                        result = "held";

                        cart.Plates.Add(plate);
                        SessionManagement.SaveShoppingCart(cart);
                    }
                    else
                    {
                        result = "not held";
                    }
                }
            }
            catch (Exception exc)
            {
                result = "error";
                ExceptionPolicy.HandleException(exc, "Log");
            }
            finally
            {
                IDictionary<string, object> dict = new Dictionary<string, object>();
                LogEntry wsLog = new LogEntry();
                dict.Add("Plate ID (Guid)", plate.ItemID);
                dict.Add("PlateText", plate.Text);
                dict.Add("MfgText", plate.MfgText);
                dict.Add("result", result);

                wsLog.Title = "UI";
                wsLog.Message = "UIAddPlateToCart.aspx";
                wsLog.TimeStamp = DateTime.UtcNow;
                wsLog.Categories.Add("Information");
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);

                Response.CacheControl = "No-cache";
                Response.ContentType = "application/xml";
                Response.Write("<RESPONSE>");
                Response.Write("<RESULT>" + result + "</RESULT>");
                if (result == "held")
                {
                    Response.Write("<PLATEID>" + plate.ItemID + "</PLATEID>");
                }
                Response.Write("</RESPONSE>");
            }
        }
    }
}
