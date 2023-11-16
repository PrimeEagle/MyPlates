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

namespace MyPlates.Tx.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void LoginControl_Authenticate(object sender, AuthenticateEventArgs e)
        {
            bool authenticatedUser = Membership.ValidateUser(LoginControl.UserName, LoginControl.Password);
            if(authenticatedUser)
            {
                FormsAuthentication.SetAuthCookie(LoginControl.UserName, true);
                FormsAuthentication.RedirectFromLoginPage(LoginControl.UserName, LoginControl.RememberMeSet);
            }
            else
            {
                LoginControl.FailureText = "Login Failure. Please try again.";
            }
        }
    }
}
