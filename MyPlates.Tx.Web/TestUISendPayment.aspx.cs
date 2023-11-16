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
using MyPlates.Tx.Carts;

namespace MyPlates.Tx.Web
{
    public partial class TestUISendPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManagement.InitializeOrder();
            BindDropdowns();
        }

        protected void bPay_Click(object sender, EventArgs e)
        {
            Order currentOrder = SessionManagement.RetrieveOrder();

            currentOrder.BillingInfo.NameFirst = this.FirstNameBox.Text;
            currentOrder.BillingInfo.NameLast = this.LastNameBox.Text;
            currentOrder.BillingInfo.Street1 = this.Street1Box.Text;
            currentOrder.BillingInfo.Street2 = this.Street2Box.Text;
            currentOrder.BillingInfo.City = this.CityBox.Text;
            currentOrder.BillingInfo.State = this.StateBox.Text.ToUpper();
            currentOrder.BillingInfo.ZIP = this.ZIPBox.Text;
            currentOrder.BillingInfo.Phone = this.PhoneBox.Text;
            currentOrder.BillingInfo.Email = this.EmailBox.Text;

            currentOrder.BillingInfo.CreditCardInfo.Number = PlateConfiguration.CleanNumber(this.CCNumberBox.Text);
            currentOrder.BillingInfo.CreditCardInfo.Name = this.CCNameBox.Text;
            currentOrder.BillingInfo.CreditCardInfo.BillingAddress1 = this.Street1Box.Text;
            currentOrder.BillingInfo.CreditCardInfo.BillingAddress2 = this.Street2Box.Text;
            currentOrder.BillingInfo.CreditCardInfo.BillingCity = this.CityBox.Text;
            currentOrder.BillingInfo.CreditCardInfo.BillingState = this.StateBox.Text.ToUpper();
            currentOrder.BillingInfo.CreditCardInfo.BillingZipCode = this.ZIPBox.Text;
            currentOrder.BillingInfo.CreditCardInfo.CardType = this.CCTypeDropdown.SelectedValue;
            currentOrder.BillingInfo.CreditCardInfo.CVV = this.CVVBox.Text;
            currentOrder.BillingInfo.CreditCardInfo.ExpMonth = int.Parse(this.CCExpMonthDropdown.SelectedValue);
            currentOrder.BillingInfo.CreditCardInfo.ExpYear = int.Parse(this.CCExpYearDropdown.SelectedValue);

            LicensePlate testPlate = new LicensePlate();
            testPlate.TotalCost = Convert.ToDecimal(this.AmountBox.Text);
            testPlate.MfgText = this.PlateTextBox.Text;

            currentOrder.Products.Add(testPlate);
            currentOrder.TraceNumber = this.TraceNumberBox.Text;
            currentOrder.UniqueTransactionID = new Guid(this.UniqueTransactionIDBox.Text);

            if (currentOrder.FinalizePayment())
            {
                this.PaymentLabel.Text = "Order successful.";
            }
            else
            {
                this.PaymentLabel.Text = "Order failed.";
            }
        }

        private void BindDropdowns()
        {
            CCTypeDropdown.DataSource = MyPlatesData.GetCreditCardTypes();
            CCTypeDropdown.DataTextField = "CardName";
            CCTypeDropdown.DataValueField = "CardType";
            CCTypeDropdown.DataBind();

            CCExpMonthDropdown.DataSource = MyPlatesData.GetMonths();
            CCExpMonthDropdown.DataTextField = "MonthName";
            CCExpMonthDropdown.DataValueField = "MonthNum";
            CCExpMonthDropdown.DataBind();

            int curYear = DateTime.Now.Year;
            for (int i = curYear; i < curYear + 11; i++)
            {
                CCExpYearDropdown.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        protected void GenerateTraceNumber_Click(object sender, EventArgs e)
        {
            Order currentOrder = SessionManagement.RetrieveOrder();
            LicensePlate tempPlate = new LicensePlate();
            tempPlate.MfgText = this.PlateTextBox.Text;
            currentOrder.Products.Add(tempPlate);
            currentOrder.GenerateNewTraceNumber();

            this.TraceNumberBox.Text = currentOrder.TraceNumber;
        }

        protected void GenerateUniqueTransactionId_Click(object sender, EventArgs e)
        {
            this.UniqueTransactionIDBox.Text = Guid.NewGuid().ToString();
        }
    }
}
