using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MyPlates.Tx.Carts
{
    [Serializable()]
    public class PersonalInfo 
    {
        private string _nameFirst;
        private string _nameLast;
        private string _street1;
        private string _street2;
        private string _city;
        private string _county;
        private int _countyNumber;
        private string _state;
        private string _zip;
        private string _zip4;
        private string _phone;
        private string _email;

        public string NameFirst
        {
            get { return _nameFirst; }
            set { _nameFirst = value; }
        }

        public string NameLast
        {
            get { return _nameLast; }
            set { _nameLast = value; }
        }

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

        public string County
        {
            get { return _county; }
            set { _county = value; }
        }

        public int CountyNumber
        {
            get { return _countyNumber; }
            set { _countyNumber = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public string ZIP
        {
            get { return _zip; }
            set 
            {
                string zip = this.CleanNumber(value);

                if (zip.Length == 5)
                {
                    _zip = value;
                    _zip4 = string.Empty;
                }

                if (zip.Length == 9)
                {
                    _zip = value.Substring(0, 5);
                    _zip4 = value.Substring(5, 4);
                }
            }
        }

        public string ZIP4
        {
            get { return _zip4; }
            set { _zip4 = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = this.CleanNumber(value); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public PersonalInfo()
        {
        }

        public PersonalInfo(string state)
        {
            _state = state;
        }

        private string CleanNumber(string phoneNum)
        {
            string newPhoneNum = "";

            for (int i = 0; i < phoneNum.Length; i++)
            {
                if (char.IsNumber(phoneNum[i]))
                {
                    newPhoneNum += phoneNum[i];
                }
            }

            return newPhoneNum;
        }
    }
}
