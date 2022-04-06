using MediatR;

namespace User.Domain.Notifications
{
    public record UserDeletedNotification : INotification
    {
        public Guid Id { get; init; }
        public bool IsEffective { get; init; }
    }
}