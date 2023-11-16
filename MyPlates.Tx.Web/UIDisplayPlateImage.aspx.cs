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
using MyPlates.Tx.Carts;
using MyPlates.Tx.Data;
using MyPlates.Tx.Configuration;

namespace MyPlates.Tx.Web
{
    public partial class UIDisplayPlateImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid plateId = new Guid(Request.QueryString["PlateID"]);
            ShoppingCart cart = SessionManagement.RetrieveShoppingCart();
            byte[] image = null;

            if (cart.Plates.Count == 0)
            {
                image = MyPlatesData.GetPlateImage(plateId);
            }
            else
            {
                foreach (LicensePlate plate in cart.Plates)
                {
                    if (plate.ItemID == plateId)
                    {
                        image = plate.PlateImage;
                        break;
                    }
                }
            }
            if (image != null)
            {
                Response.ContentType = "image/jpeg";
                Response.Clear();
                Response.BufferOutput = true;
                Response.BinaryWrite(image);
                Response.Flush();
            }
        }
    }
}
