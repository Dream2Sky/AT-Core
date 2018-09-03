using AT_Core.Attributes;
using AT_Core.Models;
using AT_Core.Models.ViewModels;
using AT_Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace AT_Core.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ATDbContext _context;
        public AccountController(ATDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [NoAuth]
        public ResultBase<LoginResult> Login(LoginModel loginModel)
        {
            
            return new ResultBase<LoginResult>(new LoginResult() { IsAdmin = false, LoginState = true });
        }


        [HttpPost]
        [NoAuth]
        public ResultBase<bool> Logout()
        {
            return new ResultBase<bool>(true);
        }
    }
}