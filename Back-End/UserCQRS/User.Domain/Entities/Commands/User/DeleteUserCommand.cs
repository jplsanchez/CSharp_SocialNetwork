using MediatR;

namespace User.Domain.Commands.User
{
    public record DeleteUserCommand : IRequest<string>
    {
        public Guid Id { get; init; }
    }
}