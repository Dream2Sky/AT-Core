using System.Linq;
using AT_Core.Attributes;
using AT_Core.Common;
using AT_Core.Exceptions;
using AT_Core.Filters;
using AT_Core.Models;
using AT_Core.Models.Entity;
using AT_Core.Models.ViewModels;
using AT_Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AT_Core.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    [ATActionFilter]
    public class AccountController : ControllerBase
    {
        private readonly ATDbContext _context;
        public AccountController(ATDbContext context)
        {
            _context = context;
            if(!_context.Users.Any())
            {
                _context.Users.Add(new User(){UserName = "Admin", Password = EncryptUtils.GetMD5("888888"), IsAdmin = true});
                _context.SaveChanges();
            }

        }

        [HttpPost]
        public ResultBase<LoginResult> Login(LoginModel loginModel)
        {
            var user = _context.Users.FirstOrDefault(n => n.UserName.Equals(loginModel.UserName) && n.IsDisable == false);
            if (user == null)
                throw new ATException(ATEnums.ErrCode.LoginFaild, "User not found!");
            if (!user.Password.Equals(EncryptUtils.GetMD5(loginModel.Password)))
                throw new ATException(ATEnums.ErrCode.LoginFaild, "Password is invaild");

            HttpContext.Session.SetString(ATConst.AuthUserSessionKey, JsonConvert.SerializeObject(user));

            return new ResultBase<LoginResult>(new LoginResult() { UserId = user.Id, UserName = user.UserName, IsAdmin = user.IsAdmin, LoginState = true });
        }

        [HttpPost]
        [NoAuth]
        public ResultBase<bool> Logout()
        {
            if(!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ATConst.AuthUserSessionKey)))
                HttpContext.Session.Remove(ATConst.AuthUserSessionKey);
            return new ResultBase<bool>(true);
        }
    }
}