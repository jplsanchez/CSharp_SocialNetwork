using MediatR;

namespace User.Domain.Commands.User
{
    public class DeleteUserCommand : IRequest<string>
    {
        public Guid Id { get; init; }
    }
}