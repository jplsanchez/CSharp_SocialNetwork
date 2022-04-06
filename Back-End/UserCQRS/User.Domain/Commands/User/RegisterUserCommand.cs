using MediatR;

namespace User.Domain.Commands.User
{
    public record RegisterUserCommand : IRequest<string>
    {
        public string? Name { get; init; }
        public ushort Age { get; init; }
        public char Gender { get; init; }
    }
}