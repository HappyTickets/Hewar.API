using Application.Notifications.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications
{
    internal class NotificationService: INotificationService
    {
        private readonly IHubContext<NotificationsHub, IClientNotificationMethods> _notificationHub;

        public NotificationService(IHubContext<NotificationsHub, IClientNotificationMethods> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public Task NotifyUserAsync(long userId, string role, NotificationDto notification)
            => _notificationHub.Clients.Group($"{userId}-{role}").ReceiveNotification(notification);

        public Task NotifyUserNotificationReadAsync(long userId, string role, long notificationId)
            => _notificationHub.Clients.Group($"{userId}-{role}").MarkNotificationAsRead(notificationId);
    }
}
