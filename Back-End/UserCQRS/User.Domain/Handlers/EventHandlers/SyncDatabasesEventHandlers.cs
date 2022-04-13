using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly IDatabasesSyncRepo<UserModel> _repository;
        private readonly ILogger<SyncDatabasesEventHandlers> _logger;

        public SyncDatabasesEventHandlers(IDatabasesSyncRepo<UserModel> repository, ILogger<SyncDatabasesEventHandlers> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(UserCreatedNotification notification, CancellationToken cancelToken)
        {
            try
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

                    await _repository.Add(user, cancelToken);
                    _logger.LogInformation("Successfully synced to read-database");
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex);
            }
        }

        public async Task Handle(UserUpdatedNotification notification, CancellationToken cancelToken)
        {
            try
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

                    await _repository.Edit(user, cancelToken);
                    _logger.LogInformation("Successfully synced to read-database");
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex);
            }
        }

        public async Task Handle(UserDeletedNotification notification, CancellationToken cancelToken)
        {
            try
            {
                if (notification.IsEffective)
                {
                    await _repository.Delete(notification.Id, cancelToken);
                    _logger.LogInformation("Successfully synced to read-database");
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex);
            }
        }

        private void LogErrorMessage(Exception ex)
        {
            string message = $"ERROR - Failed to sync read/write databases: '{ex.Message} \n {ex.StackTrace}'";
            _logger.LogError(message);
        }
    }
}