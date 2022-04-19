using MediatR;

namespace User.Domain.Notifications
{
    public record UserCreatedNotification : INotification
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public DateTime Birth { get; set; }
        public string? Email { get; set; }
        public char Gender { get; init; }
        public bool IsEffective { get; init; }
    }
}