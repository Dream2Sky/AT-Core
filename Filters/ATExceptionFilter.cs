using AT_Core.Common;
using AT_Core.Exceptions;
using AT_Core.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AT_Core.Filters
{
    public class ATExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                var result = new ResultBase<object>();
                var code = ATEnums.ErrCode.UnKnowException;
                var msg = ATEnums.ErrCode.UnKnowException.ToString();

                if (context.Exception is ATException)
                {
                    var atExp = context.Exception as ATException;
                    code = atExp.ErrCode;
                    msg = atExp.Msg;
                }
                
                result.SetMsg(code, msg);
                context.Result = new JsonResult(result);
            }

            context.ExceptionHandled = true;
        }
    }
}