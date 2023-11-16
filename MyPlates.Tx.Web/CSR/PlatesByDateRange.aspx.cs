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
    public partial class PlatesByDateRange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void FromDateCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (FromDateCheck.Checked)
            {
                FromDate.Visible = true;
                ToDateCheck.Enabled = true;
            }
            else
            {
                FromDate.Visible = false;
                ToDateCheck.Checked = false;
                ToDateCheck.Enabled = false;
                ToDate.Visible = false;
            }
        }

        protected void ToDateCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ToDateCheck.Checked)
            {
                ToDate.Visible = true;
            }
            else
            {
                ToDate.Visible = false;
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            PlateImages.DataSource = MyPlatesData.GetPlateGuids(FromDate.SelectedDate, ToDate.SelectedDate);
            PlateImages.DataBind();
        }
    }
}
