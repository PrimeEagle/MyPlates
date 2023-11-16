using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MyPlates.Tx.Carts;

namespace MyPlates.Tx.Configuration
{
    public class SessionManagement
    {
        private static string defaultCartName = "cart";
        private static string defaultOrderName = "order";

        public static ShoppingCart RetrieveShoppingCart() 
        {
            ShoppingCart cart = (ShoppingCart)HttpContext.Current.Session[defaultCartName];
            if (cart == null)
            {
                cart = new ShoppingCart();
            }

            return cart;
        }

        public static void SaveShoppingCart(ShoppingCart cart) 
        {
            HttpContext.Current.Session[defaultCartName] = cart;
        }

        public static Order RetrieveOrder() 
        {
            Order currentOrder = (Order)HttpContext.Current.Session[defaultOrderName];
            if (currentOrder == null)
            {
                currentOrder = new Order();
            }
            return currentOrder;
        }

        public static void InitializeOrder() 
        {
            HttpContext.Current.Session.Remove(defaultOrderName);
            HttpContext.Current.Session[defaultOrderName] = new Order();
        }

        public static void SaveOrder( Order currentOrder) 
        {
            HttpContext.Current.Session[defaultOrderName] = currentOrder;
        }

        public static void RemoveShoppingCart()
        {
            HttpContext.Current.Session.Remove(defaultCartName);
            HttpContext.Current.Session.Clear();
        }

        public static void RemoveOrder()
        {
            HttpContext.Current.Session.Remove(defaultOrderName);
            HttpContext.Current.Session.Clear();
        }

        public static void ClearSession() 
        {
            if (HttpContext.Current.Session != null)
            {
                ShoppingCart cart = (ShoppingCart)HttpContext.Current.Session[defaultCartName];
                if (cart != null)
                {
                    foreach (LicensePlate p in cart.Plates)
                    {
                        PlateConfiguration.CancelPlateHold(p.MfgText, p.PlateCode);
                    }
                }

                HttpContext.Current.Session.Clear();

                SessionManagement.InitializeOrder();
            }
        }
    }
}
