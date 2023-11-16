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
using MyPlates.Tx.Helper;

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace MyPlates.Tx.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessionManagement.InitializeOrder();
            }
        }

        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
        }
    }
}
