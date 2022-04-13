using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Domain.Entities.Models;
using User.Domain.Entities.Requests;
using User.Domain.Interfaces;

namespace User.Domain.Services
{
    public class LoginService : ILoginService
    {
        public SignInManager<IdentityUser<Guid>> _signInManager;
        public ITokenService _tokenService;
        public readonly ILogger<LoginService> _logger;

        public LoginService(SignInManager<IdentityUser<Guid>> signInManager, ITokenService tokenService, ILogger<LoginService> logger)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
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
                    var roles = _signInManager.UserManager.GetRolesAsync(identityUser).Result.ToList();

                    Token token = _tokenService.GenerateToken(identityUser, 1, roles);
                    return Result.Ok().WithSuccess(token.Value);
                }
            }

            var message = $"Failed to log user: {request.Username}";
            _logger.LogInformation(message);
            return Result.Fail(message);
        }
    }
}