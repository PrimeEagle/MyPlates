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
using MyPlates.Tx.Data;

namespace MyPlates.Tx.Web.CSR
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int orderId;

            if (Request.QueryString["orderid"] != null)
            {
                orderId = Convert.ToInt32(Request.QueryString["orderid"]);
                OrderID.Text = "Order " + orderId;
                ViewReceiptLink.NavigateUrl = "/CSR/ViewReceipt.aspx?orderid=" + orderId;

                CustomerInfo.DataSource = MyPlatesData.GetCustomerForOrderId(orderId);
                CustomerInfo.DataBind();

                PlateInfo.DataSource = MyPlatesData.GetPlateInfoForOrderId(orderId);
                PlateInfo.DataBind();
            }
            else
            {
                Response.Write("Invalid Order ID.");
            }
        }

        protected bool CheckEmptyData(string text)
        {
            if (text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected string DisplayLineBreak(string text)
        {
            if (text.Length > 0)
            {
                return "<br />";
            }
            else
            {
                return "";
            }
        }
    }
}
