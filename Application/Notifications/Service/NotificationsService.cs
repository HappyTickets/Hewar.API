using Application.Notifications.Dtos;
using AutoMapper;
using Domain.Events.Notifications;

namespace Application.Notifications.Service
{
    public class NotificationsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser) : INotificationsService
    {
        public async Task<Result<Empty>> MarkAsReadAsync(long id)
        {
            var notification = await ufw.GetRepository<Notification>()
                .FirstOrDefaultAsync(n => n.Id == id && n.RecipientId == currentUser.UserId);

            if (notification == null)
                return new NotFoundError();

            notification.IsRead = true;

            notification.AddDomainEvent(new NotificationCreated(notification));
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.MarkAsRead);
        }

        public async Task<Result<IEnumerable<NotificationDto>>> GetAllAsync()
        {
            var notifications = await ufw.GetRepository<Notification>()
                .FilterAsync(n => n.RecipientId == currentUser.UserId);

            var notificationDto = mapper.Map<NotificationDto[]>(notifications);
            return Result<IEnumerable<NotificationDto>>.Success(notificationDto,
                     SuccessCodes.NotificationsReceived);
        }

        public async Task<Result<long>> CountUnReadAsync()
        {
            var count = await ufw.GetRepository<Notification>()
                .CountAsync(n => !n.IsRead && n.RecipientId == currentUser.UserId);
            return Result<long>.Success(count,
                          SuccessCodes.CountUnRead);
        }
    }
}
