using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TxDot.Web.Services
{
    public class Address
    {
        private string _street1;
        private string _street2;
        private string _city;
        private string _state;
        private string _zip;
        private string _zip4;

        public string Street1
        {
            get { return _street1; }
            set { _street1 = value; }
        }

        public string Street2
        {
            get { return _street2; }
            set { _street2 = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public string ZIP
        {
            get { return _zip; }
            set { _zip = value; }
        }

        public string ZIP4
        {
            get { return _zip4; }
            set { _zip4 = value; }
        }
    }
}
