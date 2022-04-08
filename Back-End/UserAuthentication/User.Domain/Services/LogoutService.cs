using FluentResults;
using Microsoft.AspNetCore.Identity;
using User.Domain.Interfaces;

namespace User.Domain.Services
{
    public class LogoutService : ILogoutService
    {
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<Guid>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result SignOutUser()
        {
            var resultIdentity = _signInManager.SignOutAsync();
            if (resultIdentity.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }
            return Result.Fail("Logout falhou");
        }
    }
}