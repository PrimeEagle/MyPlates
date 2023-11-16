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
using TxDot.Web.Services;
using MyPlates.Tx.Carts;
using MyPlates.Tx.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace MyPlates.Tx.Web
{
    public partial class UICheckAvailability : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LicensePlate plate = new LicensePlate();
            string result = "";
            bool validated;

            try
            {
                plate.MfgText = Request.Form["PlateText"];
                plate.PlateCode = Request.Form["PlateCode"];
                PlateConfiguration.LoadPlateData(plate);

                bool success = PlateConfiguration.CheckPlateAvailability(plate.MfgText, plate.PlateCode);

                if (success)
                {
                    result = "available";
                }
                else
                {
                    result = "unavailable";
                }
            }
            catch(Exception exc)
            {
                result = "error";
                ExceptionPolicy.HandleException(exc, "Log");
            }

            Response.CacheControl = "No-cache";
            Response.ContentType = "application/xml";
            Response.Write("<RESPONSE>");
            Response.Write("<RESULT>" + result + "</RESULT>");

            if (result == "unavailable" && plate.ValidPattern)
            {
                switch (plate.SuggestionsAlgorithm)
                {
                    case "NUMSEQVALID":
                        validated = true;
                        break;
                    default:
                        validated = false;
                        break;
                }

                List<string> suggestions = PlateConfiguration.FindSuggestions(plate.MfgText, plate.PlateCode);
                if (suggestions.Count > 0)
                {
                    Response.Write("<SUGGESTIONS>");
                }
                foreach (string s in suggestions)
                {
                    Response.Write("<SUGGESTION VALIDATED=\"" + validated.ToString() + "\">" + s + "</SUGGESTION>");
                }
                if (suggestions.Count > 0)
                {
                    Response.Write("</SUGGESTIONS>");
                }
            }

            Response.Write("</RESPONSE>");
        }
    }
}
