using AutoMapper;
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

        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IMediator mediator, IWriteRepository<UserModel> repository, IMapper mapper)
        {
            _mediator = mediator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancelToken)
        {
            UserModel user = CreateUserFromRequest(request);

            try
            {
                await _repository.Add(user, cancelToken);
                await PublishNotification(user, true, cancelToken);

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
                Id = request.Id ?? Guid.NewGuid(),
                Name = request.Name,
                Age = request.Age,
                Gender = request.Gender,
                IsEnabled = request.Enable
            };
        }

        private Task PublishNotification(UserModel user, bool isEffective, CancellationToken cancelToken = default)
        {
            var userDto = _mapper.Map<UserCreatedNotification>(user);

            return _mediator.Publish(userDto with { IsEffective = isEffective }, cancelToken);
        }
    }
}