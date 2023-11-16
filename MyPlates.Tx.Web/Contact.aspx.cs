using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MyPlates.Tx.Web
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendMail_Click(object sender, EventArgs e)
        {
            string toAddress = ConfigurationManager.AppSettings["emailForQuestions"];

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            string server = ConfigurationSettings.AppSettings["emailServer"];
            int port = Convert.ToInt32(ConfigurationSettings.AppSettings["emailPort"]);

            smtpClient.Host = server;
            smtpClient.Port = port;

            MailAddress fromAddress = new MailAddress(Email.Text, Name.Text);
            message.From = fromAddress;
            message.To.Add(toAddress);
            message.Subject = "MyPlates Question";

            message.Body = Message.Text;
            smtpClient.Send(message);

            Response.Redirect("/Contact_Success.aspx");
        }
    }
}
