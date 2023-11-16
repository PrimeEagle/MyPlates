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

namespace MyPlates.Tx.Web.CSR
{
    public partial class CountyAddressExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = TxDotWebServices.GetAllCountyMailingInfo(Session.SessionID).ToArray();
            GridView1.DataBind();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

            // Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=CountyInfo.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";

            // If you want the option to open the Excel file without saving than

            // comment out the line below

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            this.GridView1.RenderControl(htmlWrite);
            
            Response.Write(stringWrite.ToString());

            Response.End();
        }
    }
}
