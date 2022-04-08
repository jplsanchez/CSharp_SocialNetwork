using Microsoft.AspNetCore.Identity;
using User.Domain.Entities.Models;

namespace User.Domain.Interfaces
{
    public interface ITokenService
    {
        Token CreateToken(IdentityUser<Guid> user);
    }
}