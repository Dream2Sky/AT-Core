using AT_Core.Common;
using AT_Core.Exceptions;
using AT_Core.Models.Entity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AT_Core.Models
{
    public class ATContext
    {
        private HttpContext currentHttpContext { get; set; }
        public ATContext(HttpContext context)
        {
            currentHttpContext = context;
        }
        private User currentUser;
        public User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    var userSession = this.currentHttpContext.Session.GetString(ATConst.AuthUserSessionKey);
                    if (!string.IsNullOrWhiteSpace(userSession))
                    {
                        var currentUser = JsonConvert.DeserializeObject(userSession);
                    }
                }
                return currentUser;
            }
        }

        public void SetCurrentUser(User user)
        {
            if (user == null)
            {
                throw new ATException(ATEnums.ErrCode.AccountException, "Account exception!");
            }
            var userJson = JsonConvert.SerializeObject(user);
            currentHttpContext.Session.SetString(ATConst.AuthUserSessionKey, userJson);
            currentUser = user;
        }

        public void ReleaseCurrentUser()
        {
            if (currentHttpContext.Session.GetString(ATConst.AuthUserSessionKey) != null)
                currentHttpContext.Session.Remove(ATConst.AuthUserSessionKey);
            currentUser = null;
        }
    }
}