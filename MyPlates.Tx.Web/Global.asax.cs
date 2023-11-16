using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using MyPlates.Tx.Carts;
using MyPlates.Tx.Configuration;

namespace MyPlates.Tx.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //Session["SessionID"] = Guid.NewGuid();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string page = HttpContext.Current.Request.ServerVariables["URL"];

            // Temporary workaround for bug in Flash
            if (Request.ServerVariables["HTTPS"] == "on" && page.Contains("CreateAPlate.aspx"))
            {
                Response.Redirect("http://" + Request.ServerVariables["SERVER_NAME"] + "/CreateAPlate.aspx");
            }
            // end Temporary workaround
            
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan maintenanceStart = Convert.ToDateTime(ConfigurationManager.AppSettings["maintenanceWindowStart"]).TimeOfDay;
            TimeSpan maintenanceEnd = Convert.ToDateTime(ConfigurationManager.AppSettings["maintenanceWindowEnd"]).TimeOfDay;

            if (currentTime.CompareTo(maintenanceStart) >= 0 && currentTime.CompareTo(maintenanceEnd) <= 0 && page.Contains(".aspx"))
            {
                Response.Redirect("/Maintenance.html");
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (Context.Error is HttpUnhandledException)
            {
                // this is an error from one of our pages (as opposed to IIS, like 404 errors)

                Exception exc = Server.GetLastError();
                ExceptionPolicy.HandleException(exc, "Log");

                if (exc is MyPlates.Tx.Carts.OrderException)
                {
                    Server.Transfer("/CartFailure.aspx");
                }
                else
                {
                    Server.Transfer("/Error.aspx");
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}