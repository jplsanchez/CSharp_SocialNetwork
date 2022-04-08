using FluentResults;

namespace User.Domain.Interfaces
{
    public interface ILogoutService
    {
        Result SignOutUser();
    }
}