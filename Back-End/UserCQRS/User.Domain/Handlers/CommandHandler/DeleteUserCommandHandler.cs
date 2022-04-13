using MediatR;
using User.Domain.Commands.User;
using User.Domain.Interfaces.Repositories;
using User.Domain.Models;
using User.Domain.Notifications;

namespace User.Domain.Handlers.CommandHandler
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IWriteRepository<UserModel> _repository;

        public DeleteUserCommandHandler(IMediator mediator, IWriteRepository<UserModel> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancelToken)
        {
            try
            {
                await _repository.Delete(request.Id, cancelToken);
                await PublishNotification(request, true, cancelToken);

                return await Task.FromResult("Usuário deletado com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, ErrorStack = ex.StackTrace });
                await PublishNotification(request, false);

                return await Task.FromResult("Ocorreu um erro no momento da exclusão");
            }
        }

        private Task PublishNotification(DeleteUserCommand request, bool isEffective, CancellationToken cancelToken = default)
        {
            return _mediator.Publish(
                new UserDeletedNotification
                {
                    Id = request.Id,
                    IsEffective = isEffective
                }, cancelToken);
        }
    }
}