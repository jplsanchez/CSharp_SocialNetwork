using FluentResults;
using Microsoft.AspNetCore.Mvc;
using User.Domain.Interfaces;

namespace User.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly ILogoutService _logoutService;

        public LogoutController(ILogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult SignOutUser()
        {
            Result result = _logoutService.SignOutUser();
            if (result.IsFailed)
            {
                return Unauthorized(result.Errors);
            }
            return Ok(result.Successes);
        }
    }
}