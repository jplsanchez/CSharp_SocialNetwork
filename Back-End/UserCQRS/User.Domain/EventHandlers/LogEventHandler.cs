using MediatR;
using User.Domain.Notifications;

namespace User.Domain.EventHandlers
{
    public class LogEventHandler :
    INotificationHandler<UserCreatedNotification>,
    INotificationHandler<UserUpdatedNotification>,
    INotificationHandler<UserDeletedNotification>,
    INotificationHandler<ErrorNotification>
    {
        public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CREATE: '{notification.Id} - {notification.Name} - {notification.Age} - {notification.Gender}'");
            });
        }

        public Task Handle(UserUpdatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"UPDATE: '{notification.Id} - {notification.Name} - {notification.Age} - {notification.Gender} - {notification.IsEffective}'");
            });
        }

        public Task Handle(UserDeletedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"DELETE: '{notification.Id} - {notification.IsEffective}'");
            });
        }

        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.ExceptionMessage} \n {notification.ErrorStack}'");
            });
        }
    }
}
