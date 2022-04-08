using AutoMapper;
using FluentResults;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using User.Domain.Entities.DTO;
using User.Domain.Entities.Models;
using User.Domain.Interfaces;
using User.Shared.Events;

namespace User.Domain.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<Guid>> userManager, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _userManager = userManager;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result> RegisterUser(CreateUserDTO userDto)
        {
            IdentityUser<Guid> identityUser = _mapper.Map<IdentityUser<Guid>>(userDto);

            if (identityUser.Id == Guid.Empty) identityUser.Id = Guid.NewGuid();

            Task<IdentityResult> identityResult = _userManager.CreateAsync(identityUser, userDto.Password);

            if (identityResult.Result.Succeeded)
            {
                await _publishEndpoint.Publish(new CreatedUser { Id = identityUser.Id, Name = identityUser.UserName, CreatedAt = DateTime.UtcNow });
                Console.WriteLine($"User created: {identityUser.Id}");
                return Result.Ok();
            }
            return Result.Fail("Fail while registering user");
        }
    }
}