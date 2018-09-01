using AT_Core.Common;

namespace AT_Core.Results
{
    public class ResultBase<T>
    {
        public ResultBase(T data)
        {
            Success = true;
            Code = ATEnums.ErrCode.LoginSuccess;
            Data = data;
        }

        public bool Success { get; set; }
        public ATEnums.ErrCode Code { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }

        public void SetMsg(ATEnums.ErrCode code, string msg)
        {
            this.Success = false;
            this.Code = code;
            this.Msg = msg;
        }
    }
}