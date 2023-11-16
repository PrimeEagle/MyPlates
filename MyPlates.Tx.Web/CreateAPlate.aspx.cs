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
using MyPlates.Tx.Configuration;
using MyPlates.Tx.Carts;

namespace MyPlates.Tx.Web
{
    public partial class CreateAPlate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            throw new OrderException("Order failed.", Server.GetLastError());
        }
    }
}
