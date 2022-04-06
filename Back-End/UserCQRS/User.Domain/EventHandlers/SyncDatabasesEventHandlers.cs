using MediatR;
using User.Domain.Interfaces.Sync;
using User.Domain.Models;
using User.Domain.Notifications;

namespace User.Domain.EventHandlers
{
    public class SyncDatabasesEventHandlers :
    INotificationHandler<UserCreatedNotification>,
    INotificationHandler<UserUpdatedNotification>,
    INotificationHandler<UserDeletedNotification>
    {
        private readonly IDatabasesSync<UserModel> _repository;

        public SyncDatabasesEventHandlers(IDatabasesSync<UserModel> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.IsEffective)
            {
                UserModel user = new()
                {
                    Id = notification.Id,
                    Name = notification.Name,
                    Age = notification.Age,
                    Gender = notification.Gender,
                };
                await _repository.Add(user, cancellationToken);
            }
        }

        public async Task Handle(UserUpdatedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.IsEffective)
            {
                UserModel user = new()
                {
                    Id = notification.Id,
                    Name = notification.Name,
                    Age = notification.Age,
                    Gender = notification.Gender,
                };
                await _repository.Edit(user, cancellationToken);
            }
        }

        public async Task Handle(UserDeletedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.IsEffective)
            {
                await _repository.Delete(notification.Id, cancellationToken);
            }
        }
    }
}