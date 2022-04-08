using FluentResults;
using Microsoft.AspNetCore.Identity;
using User.Domain.Entities.Models;
using User.Domain.Entities.Requests;
using User.Domain.Interfaces;
using System.Linq;

namespace User.Domain.Services
{
    public class LoginService : ILoginService
    {
        public SignInManager<IdentityUser<Guid>> _signInManager;
        public ITokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<Guid>> signInManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogUser(LoginRequest request)
        {
            var identityResult = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false);

            if (identityResult.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user =>
                    user.NormalizedUserName == request.Username.ToUpper());

                if (identityUser != null)
                {
                    Token token = _tokenService.CreateToken(identityUser);
                    return Result.Ok().WithSuccess(token.Value);
                }
            }
            return Result.Fail("Failed to Login");
        }
    }
}