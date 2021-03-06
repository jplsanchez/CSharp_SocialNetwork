using AutoMapper;
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
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IMediator mediator, IWriteRepository<UserModel> repository, IMapper mapper)
        {
            _mediator = mediator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancelToken)
        {
            UserModel user = CreateUserFromRequest(request);

            try
            {
                await _repository.Edit(user, cancelToken);
                await PublishNotification(user, true, cancelToken);

                return await Task.FromResult("Usuário alterado com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, ErrorStack = ex.StackTrace });
                await PublishNotification(user, false);

                return await Task.FromResult("Ocorreu um erro no momento da alteração: " + ex.Message);
            }
        }

        private UserModel CreateUserFromRequest(UpdateUserCommand request)
        {
            //TODO: Passar mapper para o código ao invés daqui
            return _mapper.Map<UserModel>(request);

            //return new()
            //{
            //    Id = request.Id,
            //    Name = request.Name,
            //    Age = request.Age,
            //    Gender = request.Gender,
            //    IsEnabled = request.Enable
            //};
        }

        private Task PublishNotification(UserModel user, bool isEffective, CancellationToken cancelToken = default)
        {
            var userDto = _mapper.Map<UserUpdatedNotification>(user);

            return _mediator.Publish(userDto with { IsEffective = isEffective }, cancelToken);
        }
    }
}