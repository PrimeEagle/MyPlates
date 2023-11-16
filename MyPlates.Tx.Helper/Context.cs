using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace MyPlates.Tx.Helper
{
    public enum UserType { INTERNET = 1, CSR = 2 }
    
    public class Context
    {
        public static string Username
        {
            get
            {
                string userName = HttpContext.Current.User.Identity.Name;
                if (userName == string.Empty)
                    return ConfigurationManager.AppSettings["DefaultUsername"];
                else
                    return userName;
            }
        }

        public static string UserHostAddress
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }

        public static string SessionID
        {
            get { return HttpContext.Current.Session.SessionID; }
        }

        public static UserType CurrentUserType
        {
            get
            {
                if (Context.Username == ConfigurationManager.AppSettings["DefaultUsername"])
                {
                    return UserType.INTERNET;
                }
                else
                {
                    return UserType.CSR;
                }
            }
        }

    }
}
