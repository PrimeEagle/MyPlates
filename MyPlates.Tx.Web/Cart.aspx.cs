using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MyPlates.Tx.Configuration;
using MyPlates.Tx.Carts;
using MyPlates.Tx.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace MyPlates.Tx.Web
{
    public partial class Cart : System.Web.UI.Page
    {
        private int itemcount = 0;
        private int ownerInfoCount = 0;
        private static int numInvalidAttempts;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "No-cache";
            LoadCart();
            if (!Page.IsPostBack)
            {
                numInvalidAttempts = 0;
                PlaceOrder.Attributes.Add("onclick", "$('#checkout :input[name=\"PlaceOrder\"]').addClass('disabled'); this.disabled=true;" + ClientScript.GetPostBackEventReference(PlaceOrder, "").ToString());
                BindData();
            }
            else
            {
                // renew hold on each plate in case of multiple post-backs (due to credit card invalid, for example) that may last
                // longer than 15 minutes and cause the plate hold(s) to expire. If a plate fails to renew, throw an exception, which will then clear the cart.
                // Otherwise, it's possible to end up with two plates with the same plate text in the cart.
                
                ShoppingCart cart = SessionManagement.RetrieveShoppingCart();

                foreach(LicensePlate plate in cart.Plates) {
                    if (!PlateConfiguration.RenewHoldPlate(plate.MfgText, plate.PlateCode))
                    {
                        throw new Exception("One or more plates expired and were removed from the cart.");
                    }
                }
            }
        }

        private void BindData()
        {
            state.DataSource = MyPlatesData.GetStates();
            state.DataTextField = "State_ID";
            state.DataValueField = "State_ID";
            state.DataBind();

            card_type.DataSource = MyPlatesData.GetCreditCardTypes();
            card_type.DataTextField = "CardName";
            card_type.DataValueField = "CardType";
            card_type.DataBind();

            card_exp_month.DataSource = MyPlatesData.GetMonths();
            card_exp_month.DataTextField = "MonthName";
            card_exp_month.DataValueField = "MonthNum";
            card_exp_month.DataBind();

            int curYear = DateTime.Now.Year;
            for (int i = curYear; i < curYear + 11; i++)
            {
                card_exp_year.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void LoadCart()
        {
            ShoppingCart cart = SessionManagement.RetrieveShoppingCart();
            if (cart.Plates.Count == 0)
            {
                Response.Redirect("/Default.aspx", false);
            }

            List<LicensePlate> plates = new List<LicensePlate>();

            foreach (IBuyable product in cart.Plates)
            {
                LicensePlate plate = (LicensePlate)product;
                PlateConfiguration.LoadPlateData(plate);

                plates.Add(plate);
            }

            LicensePlate[] plateData = plates.ToArray();
            CartList.DataKeyField = "ItemID";
            CartList.DataSource = plateData;
            CartList.DataSourceID = null;
            CartList.DataBind();

            OwnerList.RepeatDirection = RepeatDirection.Horizontal;
            OwnerList.RepeatColumns = 2;
            OwnerList.DataKeyField = "ItemID";
            OwnerList.DataSource = plateData;
            OwnerList.DataSourceID = null;
            OwnerList.DataBind();

            OrderTotal.Text = string.Format("{0:C}", cart.TotalCost);
        }

        protected string CreateFlashPlateViewScript()
        {
            ShoppingCart cart = SessionManagement.RetrieveShoppingCart();
            string script = "<script type=\"text/javascript\">";

            foreach (LicensePlate plate in cart.Plates)
            {
                script += "var flashvars = {";
                script += "    plateClass: \"" + plate.Class + "\",";
                script += "    characterCombination: \"" + plate.MfgText + "\"";
                script += "};";
                script += "var params = {";
                script += "    menu: \"false\"";
                script += "};";
                script += "var attributes = {};";
                script += "swfobject.embedSWF(\"flash/plateview.swf\", \"plateview" + plate.ItemID + "\", \"127\", \"66\", \"9.0.0\", \"flash/expressInstall.swf\", flashvars, params, attributes);";
            }
            script += "</script>";

            return script;
        }

        protected void CartList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                Guid itemId = new Guid(e.CommandArgument.ToString());
                ShoppingCart cart = SessionManagement.RetrieveShoppingCart();

                int idx = 0;
                foreach (LicensePlate plate in cart.Plates)
                {
                    if (plate.ItemID == itemId)
                    {
                        PlateConfiguration.CancelPlateHold(plate.MfgText, plate.PlateCode);
                        break;
                    }
                    idx++;
                }

                cart.Plates.RemoveAt(idx);

                SessionManagement.SaveShoppingCart(cart);
                LoadCart();
            }
        }

        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;
            bool orderComplete = false;
            Order currentOrder = SessionManagement.RetrieveOrder();
            ShoppingCart cart = SessionManagement.RetrieveShoppingCart();

            if (cart == null || cart.Plates.Count == 0)
            {
                Response.Redirect("/Default.aspx", false);
            }

            //populate billing info
            currentOrder.BillingInfo.NameFirst = this.first_name.Text;
            currentOrder.BillingInfo.NameLast = this.last_name.Text;
            currentOrder.BillingInfo.Street1 = this.address_1.Text;
            currentOrder.BillingInfo.Street2 = this.address_2.Text;
            currentOrder.BillingInfo.City = this.city.Text;
            currentOrder.BillingInfo.State = this.state.SelectedValue;


            string zip = string.Empty;
            string zip4 = string.Empty;
            if (this.zip.Text != null)
            {
                string zipString = this.zip.Text;

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
            
            currentOrder.BillingInfo.ZIP = zip;
            currentOrder.BillingInfo.ZIP4 = zip4;
            currentOrder.BillingInfo.Phone = this.phone.Text;
            currentOrder.BillingInfo.Email = this.email.Text;

            currentOrder.BillingInfo.CreditCardInfo.Number = PlateConfiguration.CleanNumber(this.card_num.Text);
            currentOrder.BillingInfo.CreditCardInfo.Name = this.card_name.Text;
            currentOrder.BillingInfo.CreditCardInfo.BillingAddress1 = this.address_1.Text;
            currentOrder.BillingInfo.CreditCardInfo.BillingAddress2 = this.address_2.Text;
            currentOrder.BillingInfo.CreditCardInfo.BillingCity = this.city.Text;
            currentOrder.BillingInfo.CreditCardInfo.BillingState = this.state.SelectedValue;
            currentOrder.BillingInfo.CreditCardInfo.BillingZipCode = currentOrder.BillingInfo.ZIP;
            currentOrder.BillingInfo.CreditCardInfo.BillingZipCode4 = currentOrder.BillingInfo.ZIP4;

            currentOrder.BillingInfo.CreditCardInfo.CardType = this.card_type.SelectedValue;
            currentOrder.BillingInfo.CreditCardInfo.CVV = this.card_code.Text;
            currentOrder.BillingInfo.CreditCardInfo.ExpMonth = int.Parse(this.card_exp_month.SelectedValue);
            currentOrder.BillingInfo.CreditCardInfo.ExpYear = int.Parse(this.card_exp_year.SelectedValue);

            currentOrder.Products = cart.Plates;

            bool allPatternsValid = true;

            foreach (LicensePlate plate in cart.Plates)
            {
                if (!plate.ValidPattern)
                {
                    allPatternsValid = false;
                    break;
                }
            }

            bool allPlatesOnHold = true;
            foreach (LicensePlate p in cart.Plates)
            {
                if (!PlateConfiguration.RenewHoldPlate(p.MfgText, p.PlateCode))
                {
                    if (!PlateConfiguration.HoldPlate(p.MfgText, p.PlateCode))
                    {
                        allPlatesOnHold = false;
                        break;
                    }
                }
            }
            bool allCountiesValid = true;
            foreach (LicensePlate p in cart.Plates) 
            {
                if(p.OwnerInfo.CountyNumber <= 0) 
                {
                    allCountiesValid = false;
                    break;
                }
            }

            bool allFieldsValid = true;

            Regex rx = new Regex(@"^\d{5}(-\d{4})?$");
            if (!rx.IsMatch(currentOrder.BillingInfo.ZIP))
            {
                allFieldsValid = false;
                ResponseLabel.Text = "Invalid ZIP.";
            }

            rx = new Regex(@"^(((\(\d{3}\)((-?)|( ?)))|(\d{3}-))\d{3}-\d{4})$|^\d{10}$");
            if (!rx.IsMatch(currentOrder.BillingInfo.Phone))
            {
                allFieldsValid = false;
                ResponseLabel.Text = "Invalid Phone Number.";
            }

            rx = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!rx.IsMatch(currentOrder.BillingInfo.Email))
            {
                allFieldsValid = false;
                ResponseLabel.Text = "Invalid Email.";
            }

            rx = new Regex(@"(?=[A-Za-z])");
            if (!rx.IsMatch(currentOrder.BillingInfo.Street1) || currentOrder.BillingInfo.Street1.Length < 4)
            {
                allFieldsValid = false;
                ResponseLabel.Text = "Invalid Street Address 1 (must contain at least one letter, and have 4 or more characters).";
            }

            rx = new Regex(@"(?=[A-Za-z])");
            if ((!rx.IsMatch(currentOrder.BillingInfo.Street2) && currentOrder.BillingInfo.Street2.Length > 0) ||
                currentOrder.BillingInfo.Street2.Length > 0 && currentOrder.BillingInfo.Street2.Length < 4)
            {
                allFieldsValid = false;
                ResponseLabel.Text = "Invalid Street Address 2 (must contain at least one letter, and have 4 or more characters).";
            }

            rx = new Regex(@"^((4\d{3})|(5[1-5]\d{2})|(6011))-?\d{4}-?\d{4}-?\d{4}|3[4,7]\d{13}$");
            if (!rx.IsMatch(currentOrder.BillingInfo.CreditCardInfo.Number))
            {
                allFieldsValid = false;
                ResponseLabel.Text = "Invalid Credit Card Number.";
            }

            rx = new Regex(@"^([0-9]{3,4})$");
            if (!rx.IsMatch(currentOrder.BillingInfo.CreditCardInfo.CVV))
            {
                allFieldsValid = false;
                ResponseLabel.Text = "Invalid CVV.";
            }

            if (currentOrder.BillingInfo.NameFirst == string.Empty ||
                currentOrder.BillingInfo.NameLast == string.Empty ||
                currentOrder.BillingInfo.Street1 == string.Empty ||
                currentOrder.BillingInfo.City == string.Empty ||
                currentOrder.BillingInfo.ZIP == string.Empty ||
                currentOrder.BillingInfo.Phone == string.Empty ||
                currentOrder.BillingInfo.Email == string.Empty ||
                currentOrder.BillingInfo.CreditCardInfo.Name == string.Empty ||
                currentOrder.BillingInfo.CreditCardInfo.CardType == string.Empty ||
                currentOrder.BillingInfo.CreditCardInfo.ExpMonth == 0 ||
                currentOrder.BillingInfo.CreditCardInfo.ExpYear == 0 ||
                currentOrder.BillingInfo.CreditCardInfo.CVV == string.Empty ||
                currentOrder.BillingInfo.CreditCardInfo.Number == string.Empty
                )
            {
                allFieldsValid = false;
                ResponseLabel.Text = "All required fields were not filled in.";
            }


            bool error = !(allPatternsValid & allPlatesOnHold & allCountiesValid & allFieldsValid);

            if (error)
            {
                if (!allFieldsValid)
                {
                    errorMsg = "Page validation failed.";
                }

                if (!allCountiesValid)
                {
                    errorMsg = "Owner Info Counties are invalid.";
                    throw new OrderException(errorMsg);
                }

                if (!allPlatesOnHold)
                {
                    errorMsg = "All plates could not be placed on hold prior to ordering.";
                    throw new OrderException(errorMsg);
                }

                if (!allPatternsValid)
                {
                    errorMsg = "All plate patterns were not valid.";
                    throw new OrderException(errorMsg);
                }
            }
            else
            {
                bool paymentSuccessful = false;

                try
                {
                    currentOrder.GenerateNewTraceNumber();
                    currentOrder.RecordOrder();
                    paymentSuccessful = currentOrder.FinalizePayment();
                }
                catch
                {
                    throw;
                }

                try
                {
                    if (paymentSuccessful)
                    {
                        currentOrder.ProcessOrder();
                        SessionManagement.SaveOrder(currentOrder);
                        orderComplete = true;
                        Server.Transfer("/CartSuccess.aspx", false);
                    }
                    else
                    {
                        numInvalidAttempts++;
                        if (numInvalidAttempts >= Convert.ToInt32(ConfigurationManager.AppSettings["maxInvalidPaymentAttempts"]))
                        {
                            throw new OrderException("Too many invalid payment attempts.");
                        }
                        else 
                        {
                            ResponseLabel.Text = "The credit card information entered was denied.";
                        }
                    }
                }
                catch
                {
                    if (paymentSuccessful && !orderComplete)
                    {
                        LogEntry emailLog = new LogEntry();
                        emailLog.Categories.Add("Notification");
                        emailLog.TimeStamp = DateTime.Now;
                        emailLog.Title = "INCOMPLETE ORDER: " + currentOrder.OrderID;
                        emailLog.Message = "The payment for Order ID " + currentOrder.OrderID + " was processed successfully, but it could not be sent to TxDOT.";

                        Logger.Write(emailLog);
                    }
                    throw;
                }
            }
        }

        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            throw new OrderException("Order failed.", Server.GetLastError());
        }

        protected string DisplayFirstItemOpeningDiv()
        {
            string text = string.Empty;

            if (itemcount == 1)
            {
                text = "<div class=\"item\">";
            }
            else
            {
                text = "<div class=\"item divider\">";
            }

            return text;
        }

        protected string GetNaturalTexasDisclaimer(string plateCode)
        {
            string text = string.Empty;

            if (plateCode == "PLPA107" || plateCode == "PLPC107")
            {
                text = "<em>The Texas Parks and Wildlife Department receives no direct proceeds from the sale of this plate.</em>";
            }

            return text;
        }

        protected string DisplayFirstItemClosingDiv()
        {
            string text = string.Empty;

            text = "</div>";

            return text;
        }

        protected string DisplayOwnerInfoLineBreak()
        {
            string text = string.Empty;

            if ((ownerInfoCount % 2 == 0) && ownerInfoCount > 0)
            {
                text = "<br class=\"clear\" />";
            }

            return text;
        }

        protected void CartList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                itemcount++;
            }
        }

        protected void OwnerList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                ownerInfoCount++;
            }
        }
    }
}
