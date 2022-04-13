using MassTransit;
using MediatR;
using User.Domain.Commands.User;
using User.Domain.Entities.Commands.Extensions;
using User.Shared.Events;

namespace User.Application.QueueHandlers
{
    public class UserConsumer : IConsumer<CreatedUser>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserConsumer> _logger;

        public UserConsumer(IMediator mediator, ILogger<UserConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreatedUser> context)
        {
            var command = new RegisterUserCommand
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Enable = false
            };

            if (!command.IsValid())
            {
                _logger.LogError("RabbitMQ command is not valid");
                return;
            }

            _logger.LogInformation("Received user creation from queue: '{Id} - {Name}'", command.Id, command.Name);
            var response = await _mediator.Send(command);
        }
    }
}