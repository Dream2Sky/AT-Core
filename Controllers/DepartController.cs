using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AT_Core.Common;
using AT_Core.Exceptions;
using AT_Core.Models;
using AT_Core.Models.Entity;
using AT_Core.Models.ViewModels;
using AT_Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace AT_Core.Controllers
{
    public class DepartController : ATControllerBase
    {
        public DepartController(ATDbContext context) : base(context)
        {
        }

        [HttpPost]
        public ResultWrapper<IList<Department>> GetDepratments()
        {
            return new ResultWrapper<IList<Department>>(context.Departments.Where(n => n.IsDisable == false).ToList());
        }

        [HttpPost]
        public ResultWrapper<bool> AddDepratment(DepartmentModel departModel)
        {
            if (!string.IsNullOrWhiteSpace(departModel.DepartName))
            {
                var count = context.Departments.Count(n => n.DepartName.Equals(departModel.DepartName.Trim()));
                if (count > 0)
                {
                    throw new ATException(ATEnums.ErrCode.ObjectIsExist, string.Format("{0} has exsited",departModel.DepartName));
                }
                var depart = new Department();
                depart.DepartName = departModel.DepartName;

                context.Departments.Add(depart);
                context.SaveChanges();

                return new ResultWrapper<bool>(true);
            }
            else
                throw new ATException(ATEnums.ErrCode.InvaildInput, "departName is invaild");

        }
    }
}