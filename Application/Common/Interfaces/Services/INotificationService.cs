using Application.Notifications.Dtos;

namespace Application.Common.Interfaces.Services
{
    public interface INotificationService
    {
        Task NotifyUserAsync(long userId, string role, NotificationDto notification);
        Task NotifyUserNotificationReadAsync(long userId, string role, long notificationId);
    }
}
