using System;
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
    public class AccountController : ATControllerBase
    {
        public AccountController(ATDbContext context) : base(context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User() { UserName = "Admin", Password = EncryptUtils.GetMD5("888888"), IsAdmin = true });
                context.SaveChanges();
            }
        }

        [HttpPost]
        [NoAuth]
        public ResultWrapper<LoginResult> Login(LoginModel loginModel)
        {
            var user = context.Users.FirstOrDefault(n => n.UserName.Equals(loginModel.UserName) && n.IsDisable == false);
            if (user == null)
                throw new ATException(ATEnums.ErrCode.LoginFaild, "User not found!");
            if (!user.Password.Equals(EncryptUtils.GetMD5(loginModel.Password)))
                throw new ATException(ATEnums.ErrCode.LoginFaild, "Password is invaild");

            HttpContext.Session.SetString(ATConst.AuthUserSessionKey, JsonConvert.SerializeObject(user));
            logger.Info(user.UserName + " loging");
            return new ResultWrapper<LoginResult>(new LoginResult() { UserId = user.Id, UserName = user.UserName, IsAdmin = user.IsAdmin, LoginState = true });
        }

        [HttpPost]
        [NoAuth]
        public ResultWrapper<bool> Logout()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ATConst.AuthUserSessionKey)))
                HttpContext.Session.Remove(ATConst.AuthUserSessionKey);
            return new ResultWrapper<bool>(true);
        }

        [HttpGet]
        public ResultWrapper<string> GetCurrentUser()
        {
            var userName = "I don't know";
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ATConst.AuthUserSessionKey)))
            {
                var currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString(ATConst.AuthUserSessionKey));
                userName = currentUser.UserName;
            }

            return new ResultWrapper<string>(userName);
        }
    }
}