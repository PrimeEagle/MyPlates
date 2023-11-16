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
using TxDot.Web.Services;

namespace MyPlates.Tx.Web
{
    public partial class UIGetCountyInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = string.Empty;
            CountyInfo cInfo = new CountyInfo();

            try
            {
                string county = Request.Form["county"];
                cInfo = TxDotWebServices.GetSpecificCountyInfo(Session.SessionID, county.ToUpper());

                result = "success";
            }
            catch
            {
                result = "error";
            }

            Response.CacheControl = "No-cache";
            Response.ContentType = "application/xml";
            Response.Write("<RESPONSE>");
            Response.Write("<RESULT>" + result + "</RESULT>");
            if (result == "success")
            {
                Response.Write("<STREET1>" + cInfo.PhysicalAddress.Street1 + "</STREET1>");
                Response.Write("<STREET2>" + cInfo.PhysicalAddress.Street2 + "</STREET2>");
                Response.Write("<CITY>" + cInfo.PhysicalAddress.City + "</CITY>");
                Response.Write("<STATE>" + cInfo.PhysicalAddress.State + "</STATE>");
                string zip = cInfo.PhysicalAddress.ZIP;
                if (cInfo.PhysicalAddress.ZIP4.Length > 0)
                {
                    zip = zip + "-" + cInfo.PhysicalAddress.ZIP4;
                }
                Response.Write("<ZIP>" + zip + "</ZIP>");
                Response.Write("<PHONE>" + cInfo.Phone + "</PHONE>");
                Response.Write("<EMAIL>" + cInfo.Email + "</EMAIL>");
            }
            Response.Write("</RESPONSE>");


        }
    }
}
