using AutoMapper;
using Microsoft.AspNetCore.Identity;
using User.Domain.Entities.DTO;
using User.Shared.Events;

namespace User.Domain.Entities.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, IdentityUser<Guid>>();
            CreateMap<IdentityUser<Guid>, CreatedUser>()
                .ForMember(
                dest => dest.Name,
                act => act.MapFrom(
                    src => src.UserName
                ))
                .ForMember(
                dest => dest.CreatedAt,
                act => act.MapFrom(
                    src => DateTime.UtcNow
                ));
        }
    }
}