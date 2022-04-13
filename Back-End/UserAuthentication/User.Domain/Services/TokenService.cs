using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Domain.Entities.Models;
using User.Domain.Interfaces;

namespace User.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly IConfiguration _configuration;

        public TokenService(ILogger<TokenService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Token GenerateToken(IdentityUser<Guid> user, int expirationTime)
        {
            try
            {
                Claim[] userClaims = SetClaims(user);
                SymmetricSecurityKey key = SetSecurityKey(_configuration);
                SigningCredentials credencials = DefineCredentials(key);

                var token = new JwtSecurityToken(
                    claims: userClaims,
                    signingCredentials: credencials,
                    expires: DateTime.UtcNow.AddHours(expirationTime)
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return new Token(tokenString);
            }
            catch (Exception ex)
            {
                string message = $"ERROR - Failed to create Token: '{ex.Message} \n {ex.StackTrace}'";
                _logger.LogError(message);
                throw;
            }
        }

        public Token GenerateToken(IdentityUser<Guid> user, int expirationTime, IEnumerable<string> roles)
        {
            try
            {
                Claim[] userClaims = SetClaims(user);
                SymmetricSecurityKey key = SetSecurityKey(_configuration);
                SigningCredentials credencials = DefineCredentials(key);

                var token = new JwtSecurityToken(
                    claims: userClaims,
                    signingCredentials: credencials,
                    expires: DateTime.UtcNow.AddHours(expirationTime)
                    );

                if (roles != null && roles.Any())
                    token.Payload["roles"] = roles;

                var tokenHandler = new JwtSecurityTokenHandler();

                return new Token(
                    tokenHandler.WriteToken(token)
                    );
            }
            catch (Exception ex)
            {
                string message = $"ERROR - Failed to create Token: '{ex.Message} \n {ex.StackTrace}'";
                _logger.LogError(message);
                throw;
            }
        }

        private static Claim[] SetClaims(IdentityUser<Guid> user)
        {
            return new Claim[]
            {
                    new Claim("username", user.UserName),
                    new Claim("id", user.Id.ToString())
            };
        }

        private static SymmetricSecurityKey SetSecurityKey(IConfiguration configuration)
        {
            var defaultKey = configuration.GetSection("Key:DefaultKey").Value;

            return new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(defaultKey)
                );
        }

        private static SigningCredentials DefineCredentials(SymmetricSecurityKey key)
        {
            return new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
                );
        }
    }
}