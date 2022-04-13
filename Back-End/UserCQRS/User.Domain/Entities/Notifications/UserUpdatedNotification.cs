using MediatR;

namespace User.Domain.Notifications
{
    public record UserUpdatedNotification : INotification
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public ushort Age { get; init; }
        public char Gender { get; init; }
        public bool IsEffective { get; init; }
    }
}