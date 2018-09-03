using System;
using AT_Core.Common;

namespace AT_Core.Exceptions
{
    public class ATException : Exception
    {
        public ATException(ATEnums.ErrCode code, string msg)
        {
            ErrCode = code;
            Msg = msg;
        }
        public ATEnums.ErrCode ErrCode { get; set; }
        public string Msg { get; set; }
    }
}