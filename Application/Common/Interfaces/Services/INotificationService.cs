using Application.Notifications.Dtos;

namespace Application.Common.Interfaces.Services
{
    public interface INotificationService
    {
        Task NotifyAllUsersAsync(NotificationDto notification);
        Task NotifyUserAsync(long entityId, EntityTypes entityType, NotificationDto notification);
        Task NotifyUserNotificationReadAsync(long entityId, EntityTypes entityType, long notificationId);
    }
}
