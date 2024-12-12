using Application.Notifications.Dtos;

namespace Application.Notifications.Service
{
    public interface INotificationsService
    {
        Task<Result<long>> CountUnReadAsync();
        Task<Result<IEnumerable<NotificationDto>>> GetAllAsync();
        Task<Result<Empty>> MarkAsReadAsync(long id);
    }
}
