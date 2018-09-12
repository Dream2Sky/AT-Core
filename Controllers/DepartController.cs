using System.Collections;
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
    public class DepartController : ATControllerBase
    {
        public DepartController(ATDbContext dbContext, IHttpContextAccessor accessor) 
            : base(dbContext, accessor)
        {
        }

        [HttpGet]
        public IList<DepartmentOutput> GetDepratments()
        {
            var departList = DBContext.Departments.Where(z => z.IsDisable == false).Select(z => new DepartmentOutput()
            {
                DepartId = z.Id,
                DepartName = z.DepartName
            }).ToList();
            return new List<DepartmentOutput>(departList);
        }

        [HttpPost]
        public bool AddDepratment([FromBody]string departName)
        {
            if (!string.IsNullOrWhiteSpace(departName))
            {
                var count = DBContext.Departments.Count(n => n.DepartName.Equals(departName.Trim()));
                if (count > 0)
                {
                    throw new ATException(ATEnums.ErrCode.ObjectIsExist, string.Format("{0} has exsited", departName));
                }
                var depart = new Department();
                depart.DepartName = departName;

                DBContext.Departments.Add(depart);
                DBContext.SaveChanges();

                return true;
            }
            else
                throw new ATException(ATEnums.ErrCode.InvaildInput, "departName is invaild");

        }
    }
}