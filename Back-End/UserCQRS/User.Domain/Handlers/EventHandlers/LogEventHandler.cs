using MediatR;
using Microsoft.Extensions.Logging;
using User.Domain.Notifications;
using Newtonsoft.Json;

namespace User.Domain.EventHandlers
{
    public class LogEventHandler :
    INotificationHandler<UserCreatedNotification>,
    INotificationHandler<UserUpdatedNotification>,
    INotificationHandler<UserDeletedNotification>,
    INotificationHandler<ErrorNotification>
    {
        private readonly ILogger<LogEventHandler> _logger;

        public LogEventHandler(ILogger<LogEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedNotification notification, CancellationToken cancelToken)
        {
            return Task.Run(() =>
            {
                var message = $"User Created[{notification.IsEffective}]: ";
                message += JsonConvert.SerializeObject(notification);
                _logger.LogInformation(message);
            }, cancelToken);
        }

        public Task Handle(UserUpdatedNotification notification, CancellationToken cancelToken)
        {
            return Task.Run(() =>
            {
                var message = $"User Updated [{notification.IsEffective}]: ";
                message += JsonConvert.SerializeObject(notification);

                _logger.LogInformation(message);
            }, cancelToken);
        }

        public Task Handle(UserDeletedNotification notification, CancellationToken cancelToken)
        {
            return Task.Run(() =>
            {
                var message = $"User Deleted[{notification.IsEffective}]: ";
                message += JsonConvert.SerializeObject(notification);

                _logger.LogInformation(message);
            }, cancelToken);
        }

        public Task Handle(ErrorNotification notification, CancellationToken cancelToken)
        {
            return Task.Run(() =>
            {
                var message = $"ERROR: '{notification.ExceptionMessage} \n {notification.ErrorStack}'";

                _logger.LogError(message);
            }, cancelToken);
        }
    }
}