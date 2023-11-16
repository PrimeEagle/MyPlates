using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPlates.Tx.Data;
using MyPlates.Tx.ePay;
using TxDot.Web.Services;

namespace MyPlates.Tx.Carts
{
    [Serializable()]
    public class ShoppingCart
    {
        List<IBuyable> _plates = new List<IBuyable>();

        public List<IBuyable> Plates
        {
            get { return _plates; }
        }

        public ShoppingCart()
        {
        }

        public ShoppingCart(List<IBuyable> plateList)
        {
            _plates = plateList;
        }

        public void EmptyCart()
        {
            _plates.Clear();
        }

        public decimal TotalCost
        {
            get
            {
                decimal cost = 0;

                foreach (IBuyable plate in _plates)
                {
                    cost = cost + plate.GetTotalCost();
                }

                return cost;
            }
        }
    }
}
