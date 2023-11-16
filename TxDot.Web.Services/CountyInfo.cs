using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TxDot.Web.Services
{
    public class CountyInfo
    {
        private string _name;
        private int _number;
        private string _phone;
        private string _email;
        private Address _mailingAddress = new Address();
        private Address _physicalAddress = new Address();
        private string _tacName;

        public string TACName
        {
            get { return _tacName; }
            set { _tacName = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public Address MailingAddress
        {
            get { return _mailingAddress; }
        }

        public Address PhysicalAddress
        {
            get { return _physicalAddress; }
        }
    }
}
