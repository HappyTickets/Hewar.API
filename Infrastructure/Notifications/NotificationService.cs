using Application.Notifications.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications
{
    internal class NotificationService(IHubContext<NotificationsHub, IClientNotificationMethods> notificationHub) : INotificationService
    {
        public Task NotifyUserAsync(long entityId, EntityTypes entityType, NotificationDto notification)
            => notificationHub.Clients.Group($"{entityId}-{entityType}").ReceiveNotification(notification);

        public Task NotifyAllUsersAsync(NotificationDto notification)
            => notificationHub.Clients.All.ReceiveNotification(notification);

        public Task NotifyUserNotificationReadAsync(long entityId, EntityTypes entityType, long notificationId)
            => notificationHub.Clients.Group($"{entityId}-{entityType}").MarkNotificationAsRead(notificationId);
    }
}
