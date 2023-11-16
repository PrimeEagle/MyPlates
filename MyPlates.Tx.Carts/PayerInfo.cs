using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPlates.Tx.ePay;

namespace MyPlates.Tx.Carts
{
    [Serializable()]
    public class PayerInfo : PersonalInfo
    {
        private CreditCard _creditCardInfo = new CreditCard();

        public CreditCard CreditCardInfo
        {
            get { return _creditCardInfo; }
        }

        
    }
}
