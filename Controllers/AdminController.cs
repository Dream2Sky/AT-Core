using System.Collections.Generic;
using System.Linq;
using AT_Core.Attributes;
using AT_Core.Common;
using AT_Core.Exceptions;
using AT_Core.Models;
using AT_Core.Models.Entity;
using AT_Core.Models.ViewModels;
using AT_Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AT_Core.Controllers
{
    [Admin]
    public class AdminController : ATControllerBase
    {
        public AdminController(ATDbContext dbContext, IHttpContextAccessor accessor) :
            base(dbContext, accessor)
        {
        }

        [HttpPost]
        public bool AddUser([FromBody]string userName = "")
        {
            if (string.IsNullOrWhiteSpace(userName.Trim()))
            {
                throw new ATException(ATEnums.ErrCode.InvaildInput, "username is invaild");
            }
            if (DBContext.Users.Count(z => z.UserName.Equals(userName)) > 0)
            {
                throw new ATException(ATEnums.ErrCode.ObjectIsExist, $"{userName} is existed");
            }
            var user = new User()
            {
                UserName = userName,
                Password = ATConst.DefaultPwd
            };

            DBContext.Users.Add(user);
            DBContext.SaveChanges();

            return true;
        }

        [HttpGet]
        public IList<GetUserOutput> GetUsers()
        {
            var result = from user in DBContext.Users
                         join depart in DBContext.Departments on user.DepartmentId equals depart.Id
                         into departTemp
                         from dt in departTemp.DefaultIfEmpty()
                         select new GetUserOutput()
                         {
                             UserId = user.Id,
                             UserName = user.UserName,
                             DepartId = user.DepartmentId,
                             DepartName = dt == null ? "" : dt.DepartName,
                             IsAdmin = user.IsAdmin,
                             IsDisabled = user.IsDisable
                         };

            return new List<GetUserOutput>(result.ToList());
        }

        public void BindDepartment()
        {
            // TODO:
            // bind department and user
        }

        public bool ResetPwd()
        {
            //TODO:
            //  Add reset password logic
            //  input is typeof IList<int> ?????
            //  reset pwd is defaultpwd
            return true;
        }
    }
}
