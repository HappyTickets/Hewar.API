using Application.Notifications.Dtos;

namespace Application.Common.Interfaces.Services
{
    public interface INotificationService
    {
        Task NotifyUserAsync(long userId, AccountTypes type, NotificationDto notification);
        Task NotifyUserNotificationReadAsync(long userId, AccountTypes type, long notificationId);
    }
}
