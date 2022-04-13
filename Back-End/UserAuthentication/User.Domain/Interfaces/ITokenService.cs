using Microsoft.AspNetCore.Identity;
using User.Domain.Entities.Models;

namespace User.Domain.Interfaces
{
    public interface ITokenService
    {
        Token GenerateToken(IdentityUser<Guid> user, int expirationTime);

        Token GenerateToken(IdentityUser<Guid> user, int expirationTime, IEnumerable<string> roles);
    }
}