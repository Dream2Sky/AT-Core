using System.Linq;
using AT_Core.Attributes;
using AT_Core.Common;
using AT_Core.Controllers;
using AT_Core.Exceptions;
using AT_Core.Models;
using AT_Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AT_Core.Filters
{
    public class AuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var noauthAttributeCount = context.Filters.Count(n =>n is NoAuthAttribute);
            if(noauthAttributeCount>0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(context.HttpContext.Session.GetString(ATConst.AuthUserSessionKey)))
            {
                var result = new ResultWrapper<object>();
                var code = ATEnums.ErrCode.NoLogin;
                var msg = ATEnums.ErrCode.NoLogin.ToString();
                
                result.SetMsg(code, msg);
                context.Result = new JsonResult(result);
            }
        }
    }
}