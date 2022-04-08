using FluentResults;
using Microsoft.AspNetCore.Mvc;
using User.Domain.Entities.Requests;
using User.Domain.Interfaces;

namespace User.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogUser(LoginRequest request)
        {
            Result result = _loginService.LogUser(request);
            if (result.IsFailed)
            {
                return Unauthorized(result.Reasons);
            }
            return Ok(result.Reasons);
        }
    }
}