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
using MyPlates.Tx.Data;
using MyPlates.Tx.Configuration;
using TxDot.Web.Services;

namespace MyPlates.Tx.Web
{
    public partial class ISAOrder : System.Web.UI.Page
    {
        private string isaSymbol = ConfigurationManager.AppSettings["ISASymbol"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDropdowns();
            }
        }

        protected void bAdd_Click(object sender, EventArgs e)
        {
            if (this.CountyDropdown.SelectedValue.Length > 0)
            {
                LicensePlate plate = new LicensePlate();

                plate.CategoryID = Convert.ToInt32(CategoriesDropdown.SelectedValue);
                plate.PlateCode = PlateCodesDropdown.SelectedValue;
                plate.RenewalPeriod = Convert.ToInt32(RenewalDropdown.SelectedValue);
                if (PlateTextDropDown.SelectedValue == string.Empty)
                {
                    plate.MfgText = PlateText.Text + isaSymbol;
                }
                else
                {
                    plate.MfgText = PlateTextDropDown.SelectedValue + isaSymbol;
                }
                plate.OwnerInfo.City = CityBox.Text;
                plate.OwnerInfo.County = CountyDropdown.SelectedItem.Text;
                plate.OwnerInfo.Email = EmailBox.Text;
                plate.OwnerInfo.NameFirst = FirstNameBox.Text;
                plate.OwnerInfo.NameLast = LastNameBox.Text;
                plate.OwnerInfo.Phone = PhoneBox.Text;
                plate.OwnerInfo.Street1 = Street1Box.Text;
                plate.OwnerInfo.Street2 = Street2Box.Text;

                string zipString = this.ZIPBox.Text;
                string zip = string.Empty;
                string zip4 = string.Empty;

                if (zipString != null)
                {
                    if (zipString.Length == 5)
                    {
                        zip = zipString;
                    }

                    if (zipString.Length == 10)
                    {
                        zip = zipString.Substring(0, 5);
                        zip4 = zipString.Substring(6, 4);
                    }
                }

                plate.OwnerInfo.ZIP = zip;
                plate.OwnerInfo.ZIP4 = zip4;

                PlateConfiguration.LoadPlateData(plate);
                AddPlate(plate);
            }
            else
            {
                AddLabel.Text = "Please select a county.";
            }
        }

        private void AddPlate(LicensePlate plate)
        {
            bool success = false;
            bool available = false;

            PlateConfiguration.LoadPlateData(plate);

            if (plate.ISA)
            {
                available = PlateConfiguration.CheckPlateAvailability(plate.MfgText, plate.PlateCode);
            }
            
            if (available)
            {
                success = PlateConfiguration.HoldPlate(plate.MfgText, plate.PlateCode);
            }

            if (success)
            {
                ShoppingCart cart = SessionManagement.RetrieveShoppingCart();
                cart.Plates.Add(plate);
                SessionManagement.SaveShoppingCart(cart);

                Response.Redirect("/Cart.aspx");
            }
            else
            {
                AddLabel.Text = "Plate was not added to cart.";
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

            PlateTextDropDown.DataSource = MyPlatesData.GetAvailableISAPlates();
            PlateTextDropDown.DataTextField = "PlateText";
            PlateTextDropDown.DataValueField = "PlateText";
            PlateTextDropDown.DataBind();

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
            if (PlateTextDropDown.SelectedValue == string.Empty)
            {
                plate.MfgText = PlateText.Text + isaSymbol;
            }
            else
            {
                plate.MfgText = PlateTextDropDown.SelectedValue + isaSymbol;
            }
            plate.PlateCode = PlateCodesDropdown.SelectedValue;
            PlateConfiguration.LoadPlateData(plate);

            bool available = false;

            if (plate.ISA)
            {
                available = PlateConfiguration.CheckPlateAvailability(plate.MfgText, plate.PlateCode);
            }
            
            if (available)
            {
                ResponseLabel.Text = "Available";
            }
            else
            {
                ResponseLabel.Text = "Plate is not available.";
            }
        }

        protected void PlateTextDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PlateTextDropDown.SelectedValue == string.Empty)
            {
                PlateText.Enabled = true;
            }
            else
            {
                PlateText.Enabled = false;
            }
        }
    }
}
