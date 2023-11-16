using System;
using System.Collections;
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
using MyPlates.Tx.Data;

namespace MyPlates.Tx.Web.CSR
{
    public partial class ViewReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["orderid"] != null)
            {
                int orderId = Convert.ToInt32(Request.QueryString["orderid"]);

                try
                {
                    MemoryStream ms = new MemoryStream(MyPlatesData.GetOrderReceipt(orderId));
                    Response.ContentType = "Application/pdf";
                    ms.WriteTo(Response.OutputStream);
                    Response.Flush();

                }
                catch 
                {
                    Response.Write("No receipt exists for this order.");
                }
            }
        }
    }
}
