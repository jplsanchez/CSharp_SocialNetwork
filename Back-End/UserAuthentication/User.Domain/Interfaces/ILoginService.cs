using FluentResults;
using User.Domain.Entities.Requests;

namespace User.Domain.Interfaces
{
    public interface ILoginService
    {
        Result LogUser(LoginRequest request);
    }
}