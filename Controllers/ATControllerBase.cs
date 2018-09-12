using System;
using AT_Core.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AT_Core.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class ATControllerBase : ControllerBase
    {
        private IHttpContextAccessor _accessor;
        public ATContext context;
        public readonly ATDbContext DBContext;
        public readonly ILog logger;
        public ATControllerBase(ATDbContext dbContext, IHttpContextAccessor accessor)
        {
            this.DBContext = dbContext;
            _accessor = accessor;
            this.context = new ATContext(_accessor.HttpContext);
            logger = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(ATControllerBase));
        }


    }
}