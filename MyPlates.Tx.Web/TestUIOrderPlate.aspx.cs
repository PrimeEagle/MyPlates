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
using MyPlates.Tx.Carts;
using MyPlates.Tx.Configuration;
using MyPlates.Tx.Data;
using TxDot.Web.Services;

namespace MyPlates.Tx.Web
{
    public partial class TestUIOrderPlate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessionManagement.InitializeOrder();
                BindDropdowns();
            }
        }

        protected void bOrder_Click(object sender, EventArgs e)
        {
            LicensePlate plate = new LicensePlate();

            plate.MfgText = PlateText.Text;
            plate.PlateCode = PlateCodesDropdown.SelectedValue;

            PlateConfiguration.LoadPlateData(plate);

            plate.CategoryID = Convert.ToInt32(CategoriesDropdown.SelectedValue);
            plate.RenewalPeriod = Convert.ToInt32(RenewalDropdown.SelectedValue);
            plate.OwnerInfo.City = CityBox.Text;
            plate.OwnerInfo.County = CountyDropdown.SelectedItem.Text;
            plate.OwnerInfo.Email = EmailBox.Text;
            plate.OwnerInfo.NameFirst = FirstNameBox.Text;
            plate.OwnerInfo.NameLast = LastNameBox.Text;
            plate.OwnerInfo.Phone = PhoneBox.Text;
            plate.OwnerInfo.Street1 = Street1Box.Text;
            plate.OwnerInfo.Street2 = Street2Box.Text;
            plate.OwnerInfo.ZIP = ZIPBox.Text;

            bool held = PlateConfiguration.HoldPlate(plate.MfgText, plate.PlateCode);
            if (held)
            {
                TxDotResponse response = plate.ProcessOrder(TraceNumber.Text, 1, DateTime.Now, DateTime.Now);

                if (!response.Success)
                {
                    throw new Exception("Order failed.");
                }
                else
                {
                    OrderLabel.Text = "Plate ordered.";
                }
            }
            else
            {
                OrderLabel.Text = "Plate not held.";
            }
        }

        protected void CategoriesDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPlateCodesDropdown();
        }

        private void BindDropdowns()
        {
            CategoriesDropdown.DataSource = MyPlatesData.GetCategories();
            CategoriesDropdown.DataTextField = "Name";
            CategoriesDropdown.DataValueField = "Category_ID";
            CategoriesDropdown.DataBind();

            BindPlateCodesDropdown();

            RenewalDropdown.DataSource = MyPlatesData.GetRenewalPeriods();
            RenewalDropdown.DataTextField = "RenewalPeriod";
            RenewalDropdown.DataValueField = "RenewalPeriod";
            RenewalDropdown.DataBind();

            List<string> counties = TxDotWebServices.GetCountyInfoList(Session.SessionID);
            CountyDropdown.DataSource = counties.ToArray();
            CountyDropdown.DataBind();
        }

        private void BindPlateCodesDropdown()
        {
            PlateCodesDropdown.DataSource = MyPlatesData.GetPlateCodes(Convert.ToInt32(CategoriesDropdown.SelectedValue));
            PlateCodesDropdown.DataTextField = "PlateName";
            PlateCodesDropdown.DataValueField = "PlateCode_ID";
            PlateCodesDropdown.DataBind();
        }

        protected void CheckAvailabilityButton_Click(object sender, EventArgs e)
        {
            LicensePlate plate = new LicensePlate();
            plate.MfgText = PlateText.Text;
            plate.PlateCode = PlateCodesDropdown.SelectedValue;
            PlateConfiguration.LoadPlateData(plate);

            bool available = false;

            available = PlateConfiguration.CheckPlateAvailability(plate.MfgText, plate.PlateCode);

            if (available)
            {
                ResponseLabel.Text = "Available";
            }
            else
            {
                ResponseLabel.Text = "Plate is not available.";
            }
        }

        protected void GenerateTraceNumber_Click(object sender, EventArgs e)
        {
            LicensePlate plate = new LicensePlate();
            plate.MfgText = PlateText.Text;
            plate.PlateCode = PlateCodesDropdown.SelectedValue;
            PlateConfiguration.LoadPlateData(plate);

            TraceNumber.Text = plate.TraceNumber;
        }
    }
}
