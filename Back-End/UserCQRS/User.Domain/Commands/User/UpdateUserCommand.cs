using MediatR;

namespace User.Domain.Commands.User
{
    public record UpdateUserCommand : IRequest<string>
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public ushort Age { get; init; }
        public char Gender { get; init; }
    }
}