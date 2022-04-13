using Microsoft.AspNetCore.Mvc;
using User.Domain.Entities.DTO;
using User.Domain.Interfaces;

namespace User.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterUserController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> Registeruser(CreateUserDTO userDto)
        {
            var result = await _registerService.RegisterUser(userDto);

            if (result.IsFailed)
            {
                return StatusCode(500); 
            }
            return Ok();
        }
    }
}