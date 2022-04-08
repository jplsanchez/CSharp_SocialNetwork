using FluentResults;
using User.Domain.Entities.DTO;

namespace User.Domain.Interfaces
{
    public interface IRegisterService
    {
        Task<Result> RegisterUser(CreateUserDTO userDto);
    }
}