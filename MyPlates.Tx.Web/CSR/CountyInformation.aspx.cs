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
using MyPlates.Tx.Configuration;

namespace MyPlates.Tx.Web.CSR
{
    public partial class CountyInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<string> counties = TxDotWebServices.GetCountyInfoList(Session.SessionID);
                county.DataSource = counties.ToArray();
                county.DataBind();
            }
        }

        protected void GetInfo_Click(object sender, EventArgs e)
        {
            CountyInfo cInfo = new CountyInfo();

            cInfo = TxDotWebServices.GetSpecificCountyInfo(Session.SessionID, county.SelectedValue.ToUpper());

            string address = cInfo.Name + " County<br /><br />";

            address = address + cInfo.PhysicalAddress.Street1 + "<br />";

            if (cInfo.PhysicalAddress.Street2.Length > 0)
            {
                address = address + cInfo.PhysicalAddress.Street2 + "<br />";
            }

            address = address + cInfo.PhysicalAddress.City + ", " + cInfo.PhysicalAddress.State + " " + cInfo.PhysicalAddress.ZIP;

            if (cInfo.PhysicalAddress.ZIP4 != null && cInfo.PhysicalAddress.ZIP4.Length > 0)
            {
                address = address + cInfo.PhysicalAddress.ZIP4;
            }

            if (cInfo.Phone.Length > 0)
            {
                address = address + "<br />";
                address = address + PlateConfiguration.FormatPhoneNumber(cInfo.Phone);
            }

            CountyAddress.Text = address;
        }
    }
}
