using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;
using MyPlates.Tx.Carts;
using MyPlates.Tx.Configuration;
using MyPlates.Tx.Data;
using MyPlates.Tx.Reporting;
using TxDot.Web.Services;

namespace MyPlates.Tx.Web
{
    public partial class CartSuccess : System.Web.UI.Page
    {
        private int itemcount = 0;
        private ArrayList counties = new ArrayList();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            
            Order currentOrder = SessionManagement.RetrieveOrder();
            OrderID.Text = currentOrder.OrderID.ToString();

            EmailConfirmation(currentOrder.OrderID);
        }


        protected int GetOrderID()
        {
            Order currentOrder = SessionManagement.RetrieveOrder();
            return currentOrder.OrderID;
        }

        protected decimal GetOrderRevenue()
        {
            Order currentOrder = SessionManagement.RetrieveOrder();
            return currentOrder.CostTotal;
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            SessionManagement.RemoveShoppingCart();

            SessionManagement.InitializeOrder();
        }

        private void LoadData()
        {
            ShoppingCart cart = SessionManagement.RetrieveShoppingCart();
            Order currentOrder = SessionManagement.RetrieveOrder();

            if (cart.Plates.Count == 0)
            {
                Response.Redirect("/Default.aspx");
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

            OrderTotal.Text = string.Format("{0:C}", cart.TotalCost);

            this.CustomerName.Text = currentOrder.BillingInfo.NameFirst + " " + currentOrder.BillingInfo.NameLast;
            this.CustomerInfo.Text = currentOrder.BillingInfo.Street1 + "<br />";

            if (currentOrder.BillingInfo.Street2 != null && currentOrder.BillingInfo.Street2.Length > 0)
            {
                this.CustomerInfo.Text += currentOrder.BillingInfo.Street2 + "<br />";
            }
            this.CustomerInfo.Text +=   currentOrder.BillingInfo.City + ", " + currentOrder.BillingInfo.State + " " + currentOrder.BillingInfo.ZIP + "<br />" +
                                        PlateConfiguration.FormatPhoneNumber(currentOrder.BillingInfo.Phone) + "<br />" +
                                        currentOrder.BillingInfo.Email;
            this.CardTypeNumber.Text = currentOrder.BillingInfo.CreditCardInfo.CardType + " " + currentOrder.BillingInfo.CreditCardInfo.MaskedNumber;

            foreach (LicensePlate plate in cart.Plates)
            {
                string county = plate.OwnerInfo.County;
                counties.Add(county);
            }

            ArrayList countiesUnique = new ArrayList();
            foreach (string county in counties)
            {
                if(!countiesUnique.Contains(county)) 
                {
                    countiesUnique.Add(county);
                }
            }

            counties.Clear();

            foreach (string county in countiesUnique)
            {
                foreach (LicensePlate plate in cart.Plates)
                {
                    if (plate.OwnerInfo.County.ToUpper() == county.ToUpper())
                    {
                        CountyInfo cInfo = new CountyInfo();
                        cInfo.Name = plate.OwnerInfo.County;
                        cInfo.PhysicalAddress.Street1 = plate.CountyStreet1;
                        cInfo.PhysicalAddress.Street2 = plate.CountyStreet2;
                        cInfo.PhysicalAddress.City = plate.CountyCity;
                        cInfo.PhysicalAddress.State = plate.CountyState;
                        cInfo.PhysicalAddress.ZIP = plate.CountyZIP;
                        cInfo.Phone = plate.CountyPhone;
                        cInfo.Email = plate.CountyEmail;

                        counties.Add(cInfo);
                        CountyAddress.Text += GetCountyAddressString(cInfo, "<br />");

                        break;
                    }
                }
                
            }

            CustomerEmail.Text = currentOrder.BillingInfo.Email;
        }

        private string GetCountyAddressString(CountyInfo cInfo, string newLineString)
        {
            string address = string.Empty;

            address = address + cInfo.PhysicalAddress.Street1 + newLineString;

            if (cInfo.PhysicalAddress.Street2.Length > 0)
            {
                address = address + cInfo.PhysicalAddress.Street2 + newLineString;
            }

            address = address + cInfo.PhysicalAddress.City + ", " + cInfo.PhysicalAddress.State + " " + cInfo.PhysicalAddress.ZIP;

            if (cInfo.PhysicalAddress.ZIP4 != null && cInfo.PhysicalAddress.ZIP4.Length > 0)
            {
                address = address + cInfo.PhysicalAddress.ZIP4;
            }

            address = address + newLineString;

            if (cInfo.Phone.Length > 0)
            {
                address = address + PlateConfiguration.FormatPhoneNumber(cInfo.Phone);
            }

            address = address + newLineString + newLineString;

            return address;
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

        protected string DisplayFirstItemClosingDiv()
        {
            string text = string.Empty;

            text = "</div>";

            return text;
        }

        protected void CartList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                itemcount++;
            }
        }

        private void EmailConfirmation(int orderId) 
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["emailConfirmationEnabled"]))
            {
                DataSet ds = MyPlatesData.GetOrderInfo(orderId);

                XslCompiledTransform xslt = new XslCompiledTransform();

                XmlDocument doc = new XmlDocument();

                doc.LoadXml(ds.GetXml().Replace("&", "&amp;"));
                System.Diagnostics.Debug.Write(doc.InnerXml);
                XmlNodeList tables = doc.DocumentElement.SelectNodes("/NewDataSet/Table");

                foreach (XmlNode t in tables)
                {
                    string county = t.SelectNodes("OwnerCounty")[0].InnerText;
                    CountyInfo cInfo = new CountyInfo();
                    foreach (CountyInfo c in counties)
                    {
                        if (c.Name.ToUpper() == county.ToUpper())
                        {
                            cInfo = c;
                            break;
                        }
                    }
                    XmlElement elem = doc.CreateElement("CountyAddress");
                    elem.InnerText = GetCountyAddressString(cInfo, "<br />");
                    t.AppendChild(elem);
                }

                XmlNodeList plateGuids = doc.DocumentElement.SelectNodes("/NewDataSet/Table/PlateGuid");

                foreach (XmlNode p in plateGuids)
                {
                    p.InnerText = "http://" + ConfigurationManager.AppSettings["imageServer"] + "/UIDisplayPlateImage.aspx?PlateID=" + p.InnerText;
                }

                xslt.Load(Server.MapPath(@"~\xslt\EmailConfirmation.xslt"));

                StringWriter sw = new StringWriter();
                xslt.Transform(doc, new XsltArgumentList(), sw);

                string server = ConfigurationSettings.AppSettings["emailServer"];
                int port = Convert.ToInt32(ConfigurationSettings.AppSettings["emailPort"]);
                MailAddress fromAddress = new MailAddress(ConfigurationSettings.AppSettings["emailFromAddress"], ConfigurationSettings.AppSettings["emailFromName"]);
                string subject = ConfigurationSettings.AppSettings["emailSubject"];
                string body = sw.ToString();

                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();

                Order currentOrder = SessionManagement.RetrieveOrder();

                string defaultToAddress = ConfigurationManager.AppSettings["emailToDefaultAddress"];
                string defaultToName = ConfigurationManager.AppSettings["emailToDefaultName"];

                MailAddress toAddress;

                if(defaultToAddress.Length > 0 && defaultToName.Length > 0) {
                    toAddress = new MailAddress(defaultToAddress, defaultToName);
                } else {
                    toAddress = new MailAddress(currentOrder.BillingInfo.Email, currentOrder.BillingInfo.NameFirst + " " + currentOrder.BillingInfo.NameLast);
                }

                smtpClient.Host = server;
                smtpClient.Port = port;
                message.From = fromAddress;

                message.To.Add(toAddress);
                message.Subject = subject;

                message.Body = body;

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, new System.Net.Mime.ContentType("text/html"));
                message.AlternateViews.Add(htmlView);
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                message.Priority = MailPriority.High;

                Report receiptReport = new Report();

                Stream receipt = receiptReport.GetPDFOrderReceipt(currentOrder.OrderID);
                
                MyPlatesData.SaveOrderReceipt(currentOrder.OrderID, ((MemoryStream)receipt).ToArray());

                Attachment pdfReceipt = new Attachment(receipt, 
                                            ConfigurationManager.AppSettings["ReceiptReportAttachmentFilename"].Replace("[orderid]", currentOrder.OrderID.ToString()) + ".pdf",
                                            "application/pdf");
                message.Attachments.Add(pdfReceipt);

                smtpClient.Send(message);
            }
        }
    }
}
