using MediatR;

namespace User.Domain.Notifications
{
    public record UserCreatedNotification : INotification
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public ushort Age { get; init; }
        public char Gender { get; init; }
        public bool IsEffective { get; init; }
    }
}