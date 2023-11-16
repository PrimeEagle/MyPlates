using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MyPlates.Tx.ePay
{
    internal static class Merchant
    {
        private static int _vendorID;
        private static string _hashAlgorithm;
        private static string _hashSecret;

        static Merchant()
        {
            _vendorID = int.Parse(ConfigurationManager.AppSettings["ePayVendorID"]);
            _hashAlgorithm = ConfigurationManager.AppSettings["ePayHashAlgorithm"];
            _hashSecret = ConfigurationManager.AppSettings["ePayHashSecret"];
        }

        internal static int VendorID
        {
            get { return _vendorID; }
        }

        internal static string HashAlgorithm
        {
            get { return _hashAlgorithm; }
        }

        internal static string HashSecret
        {
            get { return _hashSecret; }
        }
    }
}
