using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class TestUIGetCountyInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            List<string> counties = TxDotWebServices.GetCountyInfoList(Session.SessionID);
            county.DataSource = counties.ToArray();
            county.DataBind();
        }

        protected void GetInfo_Click(object sender, EventArgs e)
        {
            
        }
    }
}
