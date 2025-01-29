using Domain.Events.Notifications;

namespace Application.Notifications.Events
{
    internal class NotificationReadEventHandler : INotificationHandler<NotificationRead>
    {
        private readonly INotificationService _notificationService;

        public NotificationReadEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(NotificationRead notification, CancellationToken cancellationToken)
        {
            //await _notificationService.NotifyUserNotificationReadAsync(notification.Notification.RecipientId, notification.Notification.RecipientType, notification.Notification.Id);
        }
    }
}
