using AT_Core.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace AT_Core.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class ATControllerBase : ControllerBase
    {
        public readonly ATDbContext context;
        public readonly ILog logger;

        public ATControllerBase(ATDbContext context)
        {
            this.context = context;
            logger = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(ATControllerBase));
        }
    }
}