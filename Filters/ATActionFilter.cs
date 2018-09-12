using System;
using System.IO;
using System.Linq;
using System.Text;
using AT_Core.Common;
using AT_Core.Exceptions;
using AT_Core.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AT_Core.Filters
{
    public class ATActionFilter : Attribute, IActionFilter
    {   
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var objResult = context.Result as ObjectResult;
            context.Result = new JsonResult(new ResultWrapper<object>(objResult.Value));
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            var modelState = context.ModelState.FirstOrDefault(n => n.Value.Errors.Any());
            var errorMsg = modelState.Value.Errors.First().ErrorMessage;
            throw new ATException(ATEnums.ErrCode.InvaildInput, "inputed data is invaild");
        }
    }
}