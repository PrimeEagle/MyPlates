using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TxDot.Web.Services
{
    public class TxDotResponse
    {
        private int _errorNum;
        private string _errorMsg;

        public bool Success
        {
            get
            {
                if (_errorNum == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int ErrorNum
        {
            get { return _errorNum; }
            set { _errorNum = value; }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value; }
        }

        public TxDotResponse() { }

        public TxDotResponse(int errorNum, string errorMsg)
        {
            _errorNum = errorNum;
            _errorMsg = errorMsg;
        }
    }
}
