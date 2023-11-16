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
using MyPlates.Tx.Configuration;

namespace MyPlates.Tx.Web
{
    public partial class TestUIRenewPlateHold : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PlateCode.DataSource = MyPlatesData.GetPlateCodes();
            PlateCode.DataTextField = "PlateName";
            PlateCode.DataValueField = "PlateCode_ID";
            PlateCode.DataBind();

            SessionManagement.InitializeOrder();
        }
    }
}
