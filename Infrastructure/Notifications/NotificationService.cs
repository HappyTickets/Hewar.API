using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications
{
    internal class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationsHub, IClientNotificationMethods> _notificationHub;

        public NotificationService(IHubContext<NotificationsHub, IClientNotificationMethods> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        //public Task NotifyUserAsync(long userId, AccountTypes type, NotificationDto notification)
        //    => _notificationHub.Clients.Group($"{userId}-{type}").ReceiveNotification(notification);

        //public Task NotifyUserNotificationReadAsync(long userId, AccountTypes type, long notificationId)
        //    => _notificationHub.Clients.Group($"{userId}-{type}").MarkNotificationAsRead(notificationId);
    }
}
