using MediatR;

namespace User.Domain.Commands.User
{
    public record UpdateUserCommand : IRequest<string>
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public DateTime Birth { get; set; }
        public string? Email { get; set; }
        public ushort Age { get; init; }
        public char Gender { get; init; }
        public bool Enable { get; init; } = true;
    }
}