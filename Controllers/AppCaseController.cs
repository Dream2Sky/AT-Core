using System.Collections.Generic;
using System.Linq;
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
    public class AppCaseController : ATControllerBase
    {
        public AppCaseController(ATDbContext dbContext, IHttpContextAccessor accessor)
            : base(dbContext, accessor)
        {
        }

        // TODO:
        // AddApp
        // GetApps
        // AddATCase
        // GetATCases
        // AddATAction
        // GetATActions

        [HttpPost]
        public bool AddApp([FromBody]string appName)
        {
            if (context.CurrentUser.DepartmentId <= 0)
                throw new ATException(ATEnums.ErrCode.NoDepartment, "you have not department information, please bind any department");

            if(string.IsNullOrWhiteSpace(appName))
                throw new ATException(ATEnums.ErrCode.InvaildInput, "appName is empty");

            if(DBContext.Apps.Count(z=>z.AppName.Equals(appName) && z.DepartId.Equals(context.CurrentUser.DepartmentId) && z.IsDisable == false)>0)
                throw new ATException(ATEnums.ErrCode.ObjectIsExist, $"{appName} is exsited");

            var app = new App()
            {
                DepartId = context.CurrentUser.DepartmentId,
                AppName = appName
            };

            DBContext.Apps.Add(app);
            DBContext.SaveChanges();

            return true;
        }

        [HttpGet]
        public IList<GetAppsOutput> GetApps()
        {
            var appList = DBContext.Apps.Where(z=>z.IsDisable == false 
                && z.DepartId.Equals(context.CurrentUser.DepartmentId))
                .Select(z=>new GetAppsOutput()
                {
                    AppId = z.Id,
                     AppName = z.AppName
                });

            return new List<GetAppsOutput>(appList.ToList());
        }

    }
}