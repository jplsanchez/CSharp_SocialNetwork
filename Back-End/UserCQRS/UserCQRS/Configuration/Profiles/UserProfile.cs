using AutoMapper;
using User.Domain.Models;
using User.Domain.Notifications;

namespace User.Application.Configuration.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserCreatedNotification>();
            CreateMap<UserModel, UserUpdatedNotification>();
        }
    }
}