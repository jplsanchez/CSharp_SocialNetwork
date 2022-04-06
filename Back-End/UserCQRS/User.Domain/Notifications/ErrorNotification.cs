using MediatR;

namespace User.Domain.Notifications
{
    public record ErrorNotification : INotification
    {
        public string? ExceptionMessage { get; init; }
        public string? ErrorStack { get; init; }
    }
}