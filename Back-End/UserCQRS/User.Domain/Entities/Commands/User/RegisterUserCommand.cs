using MediatR;
using User.Domain.Entities.DTO;

namespace User.Domain.Commands.User
{
    public record RegisterUserCommand : IRequest<string>
    {
        public Guid? Id { get; set; }
        public string? Name { get; init; }
        public DateTime Birth { get; set; }
        public string? Email { get; set; }
        public ushort Age { get; init; }
        public char Gender { get; init; }
        public bool Enable { get; init; } = true;
    }
}