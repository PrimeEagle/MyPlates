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

namespace MyPlates.Tx.Web
{
    public partial class TestUIViewOrderReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ViewReceipt_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream(MyPlatesData.GetOrderReceipt(Convert.ToInt32(OrderID.Text)));

            if (ms == null)
            {
                Response.Write("No receipt exists for this order.");
            }
            else
            {
                Response.ContentType = "Application/pdf";
                ms.WriteTo(Response.OutputStream);
                Response.Flush();
            }
        }
    }
}
