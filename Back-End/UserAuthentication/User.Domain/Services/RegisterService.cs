using AutoMapper;
using FluentResults;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Domain.Entities.DTO;
using User.Domain.Interfaces;
using User.Shared.Events;

namespace User.Domain.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<RegisterService> _logger;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<Guid>> userManager, IPublishEndpoint publishEndpoint, ILogger<RegisterService> logger, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task<Result> RegisterUser(CreateUserDTO userDto)
        {
            IdentityUser<Guid> user = _mapper.Map<IdentityUser<Guid>>(userDto);

            Task<IdentityResult> identityResult = CreateIdentityResult(userDto, user);

            await identityResult;
            await SetDefaultRoles(user);

            if (!identityResult.Result.Succeeded)
            {
                _logger.LogError("Create user error: {Errors}", identityResult.Result.Errors.ToString());
                return Result.Fail("Fail while registering user");
            }
            await PublishUser(user);
            return Result.Ok();
        }

        private Task<IdentityResult> CreateIdentityResult(CreateUserDTO userDto, IdentityUser<Guid> identityUser)
        {
            if (identityUser.Id == Guid.Empty) identityUser.Id = Guid.NewGuid();

            Task<IdentityResult> identityResult = _userManager.CreateAsync(identityUser, userDto.Password);
            return identityResult;
        }

        private async Task SetDefaultRoles(IdentityUser<Guid> identityUser)
        {
            await _userManager.AddToRolesAsync(identityUser, new List<string>() { "Default" });
        }

        private async Task PublishUser(IdentityUser<Guid> user)
        {
            var createdUser = _mapper.Map<CreatedUser>(user);
            await _publishEndpoint.Publish(createdUser);
            _logger.LogInformation("User created and published: {id} - {Name} - {Email}", createdUser.Id, createdUser.Name, createdUser.Email);

            //TODO: DELETE COMMENT

            //await _publishEndpoint.Publish(
            //    new CreatedUser
            //    {
            //        Id = user.Id,
            //        Name = user.UserName,
            //        CreatedAt = DateTime.UtcNow
            //    });
        }
    }
}