using MediatR;
using User.Domain.Commands.User;
using User.Domain.Interfaces.Repositories;
using User.Domain.Models;
using User.Domain.Notifications;

namespace User.Domain.Handlers.CommandHandler
{
    public class CreateUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IWriteRepository<UserModel> _repository;

        public CreateUserCommandHandler(IMediator mediator, IWriteRepository<UserModel> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            UserModel user = CreateUserFromRequest(request);
            user.Id = Guid.NewGuid();

            try
            {
                await _repository.Add(user);
                await PublishNotification(user, true);

                return await Task.FromResult("Usuário criada com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, ErrorStack = ex.StackTrace });
                await PublishNotification(user, false);

                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }

        private static UserModel CreateUserFromRequest(RegisterUserCommand request)
        {
            return new()
            {
                Name = request.Name,
                Age = request.Age,
                Gender = request.Gender
            };
        }

        private Task PublishNotification(UserModel user, bool isEffective)
        {
            return _mediator.Publish(
                new UserCreatedNotification
                {
                    Id = user.Id,
                    Name = user.Name,
                    Age = user.Age,
                    Gender = user.Gender,
                    IsEffective = isEffective
                });
        }
    }
}