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
using MyPlates.Tx.Data;
using TxDot.Web.Services;

namespace MyPlates.Tx.Web.CSR
{
    public partial class CustomerSearch : System.Web.UI.Page
    {
        private static SortDirection lastSortDirection;
        private static string lastSortExpression;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FromDate.SelectedDate = DateTime.Today;
                ToDate.SelectedDate = DateTime.Today;
                
                Username.Items.Add(new ListItem(ConfigurationManager.AppSettings["DefaultUsername"], ConfigurationManager.AppSettings["DefaultUsername"]));
                Username.DataSource = MyPlatesData.GetUsernames();
                Username.DataTextField = "UserName";
                Username.DataValueField = "UserName";
                Username.DataBind();

                List<string> counties = TxDotWebServices.GetCountyInfoList(Session.SessionID);
                county.DataSource = counties.ToArray();
                county.DataBind();
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void ResultsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ResultsGrid.PageIndex = e.NewPageIndex;
            BindData();
        }

        private DataSet BindData()
        {
            DateTime fromDate = FromDate.Visible ? FromDate.SelectedDate : DateTime.MinValue;
            DateTime toDate = ToDate.Visible ? ToDate.SelectedDate.AddDays(1).AddMilliseconds(-1) : DateTime.MinValue;

            DataSet ds = MyPlatesData.SearchCustomersOwners(LastName.Text, FirstName.Text, Email.Text, Phone.Text, ZIP.Text, 
                county.SelectedValue, Username.Text, fromDate, toDate, OrderID.Text == string.Empty ? -1 : Convert.ToInt32(OrderID.Text), PlateText.Text);
            ResultsGrid.DataSource = ds;
            ResultsGrid.DataBind();

            return ds;
        }

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        protected void ResultsGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = BindData().Tables[0];

            if (dt != null)
            {
                DataView dv = new DataView(dt);
                if (e.SortExpression == lastSortExpression)
                {
                    if (lastSortDirection == SortDirection.Ascending)
                    {
                        e.SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        e.SortDirection = SortDirection.Ascending;
                    }
                }
                dv.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                lastSortExpression = e.SortExpression;
                lastSortDirection = e.SortDirection;

                ResultsGrid.DataSource = dv;
                ResultsGrid.DataBind();
            }
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
    }
}
