using Application.Notifications.Dtos;

namespace Infrastructure.Notifications
{
    public interface IClientNotificationMethods
    {
        Task ReceiveNotification(NotificationDto notification);
        Task MarkNotificationAsRead(long notificationId);
    }
}
