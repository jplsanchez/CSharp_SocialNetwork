using MediatR;
using User.Domain.Commands.User;
using User.Domain.Interfaces.Repositories;
using User.Domain.Models;
using User.Domain.Notifications;

namespace User.Domain.Handlers.CommandHandler
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IWriteRepository<UserModel> _repository;

        public UpdateUserCommandHandler(IMediator mediator, IWriteRepository<UserModel> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            UserModel user = CreateUserFromRequest(request);

            try
            {
                await _repository.Edit(user);
                await PublishNotification(user, true);

                return await Task.FromResult("Usuário alterado com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, ErrorStack = ex.StackTrace });
                await PublishNotification(user, false);

                return await Task.FromResult("Ocorreu um erro no momento da alteração: " + ex.Message);
            }
        }

        private static UserModel CreateUserFromRequest(UpdateUserCommand request)
        {
            return new() { Id = request.Id, Name = request.Name, Age = request.Age, Gender = request.Gender };
        }

        private Task PublishNotification(UserModel user, bool isEffective)
        {
            return _mediator.Publish(
                new UserUpdatedNotification
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