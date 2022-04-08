using AutoMapper;
using Microsoft.AspNetCore.Identity;
using User.Domain.Entities.DTO;

namespace User.Domain.Entities.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, IdentityUser<Guid>>();
        }
    }
}