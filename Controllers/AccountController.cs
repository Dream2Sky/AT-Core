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
        public AccountController(ATDbContext dbContext, IHttpContextAccessor accessor) : 
            base(dbContext, accessor)
        {
        }

        [HttpPost]
        [NoAuth]
        public LoginOutput Login(LoginInput loginInput)
        {
            var user = DBContext.Users.FirstOrDefault(n => n.UserName.Equals(loginInput.UserName) && n.IsDisable == false);
            if (user == null)
                throw new ATException(ATEnums.ErrCode.LoginFaild, "User not found!");
            if (!user.Password.Equals(EncryptUtils.GetMD5(loginInput.Password)))
                throw new ATException(ATEnums.ErrCode.LoginFaild, "Password is invaild");

            context.SetCurrentUser(user);
            logger.Info(user.UserName + " loging");
            return new LoginOutput()
            {
                UserId = user.Id,
                UserName = user.UserName,
                IsAdmin = user.IsAdmin,
                LoginState = true,
                DepartId = user.DepartmentId
            };
        }

        [HttpPost]
        [NoAuth]
        public bool Logout()
        {
            context.ReleaseCurrentUser();
            return true;
        }

        [HttpGet]
        public string GetCurrentUser()
        {
            var userName = "I don't know";
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ATConst.AuthUserSessionKey)))
            {
                var currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString(ATConst.AuthUserSessionKey));
                userName = currentUser.UserName;
            }

            return userName;
        }

        public bool UpdateUser()
        {
            // TODO:
            // update user info, user.name or user.password??
            return false;
        }
    }
}