using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
using TxDot.Web.Services;
using MyPlates.Tx.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace MyPlates.Tx.Web
{
    public partial class UIRenewPlateHold : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "";
            bool overallSuccess = true;
            
            try
            {
                ShoppingCart cart = SessionManagement.RetrieveShoppingCart();

                foreach (LicensePlate plate in cart.Plates)
                {
                    bool success = PlateConfiguration.RenewHoldPlate(plate.MfgText, plate.PlateCode);
                    if (!success)
                    {
                        overallSuccess = false;
                    }
                }

                if (overallSuccess)
                {
                    result = "renewed";
                }
                else
                {
                    result = "not renewed";
                }
            }
            catch(Exception exc)
            {
                result = "error";
                ExceptionPolicy.HandleException(exc, "Log");
            }

            Response.CacheControl = "No-cache";
            Response.ContentType = "application/xml";
            Response.Write("<RESPONSE>");
            Response.Write("<RESULT>" + result + "</RESULT>");
            Response.Write("</RESPONSE>");

        }
    }
}
