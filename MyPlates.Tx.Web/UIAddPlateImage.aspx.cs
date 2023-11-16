using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MyPlates.Tx.Carts;
using MyPlates.Tx.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace MyPlates.Tx.Web
{
    public partial class UIAddPlateImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "not added";
            byte[] image;
            Guid plateId;
            int strLen, strRead;

            Stream str = Request.InputStream;
            strLen = Convert.ToInt32(str.Length);
            image = new byte[strLen];
            strRead = str.Read(image, 0, strLen);

            plateId = new Guid(Request.QueryString["PlateID"]);
            ShoppingCart cart = SessionManagement.RetrieveShoppingCart();

            try
            {
                foreach (LicensePlate plate in cart.Plates)
                {
                    if (plate.ItemID == plateId)
                    {
                        plate.PlateImage = image;
                        result = "added";
                    }
                }

                SessionManagement.SaveShoppingCart(cart);
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
                dict.Add("Plate ID (Guid)", plateId);
                dict.Add("result", result);

                wsLog.Title = "UI";
                wsLog.Message = "UIAddPlateImage.aspx";
                wsLog.TimeStamp = DateTime.UtcNow;
                wsLog.Categories.Add("Information");
                wsLog.ExtendedProperties = dict;
                Logger.Write(wsLog);

                Response.CacheControl = "No-cache";
                Response.ContentType = "application/xml";
                Response.Write("<RESPONSE><RESULT>" + result + "</RESULT></RESPONSE>");
            }
        }
    }
}
