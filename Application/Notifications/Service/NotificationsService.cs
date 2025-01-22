using Application.Notifications.Dtos;
using AutoMapper;
using Domain.Events.Notifications;

namespace Application.Notifications.Service
{
    public class NotificationsService : INotificationsService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public NotificationsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser)
        {
            _ufw = ufw;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Empty>> MarkAsReadAsync(long id)
        {
            var notification = await _ufw.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.RecipientId == _currentUser.AccountId && n.RecipientType == _currentUser.Type!.Value);

            if (notification == null)
                return new NotFoundError();

            notification.IsRead = true;

            notification.AddDomainEvent(new NotificationCreated(notification));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.MarkAsRead);
        }

        public async Task<Result<IEnumerable<NotificationDto>>> GetAllAsync()
        {
            var notifications = await _ufw.Notifications
                .FilterAsync(n => n.RecipientId == _currentUser.AccountId && n.RecipientType == _currentUser.Type!.Value);

            var notificationDto = _mapper.Map<NotificationDto[]>(notifications);
            return Result<IEnumerable<NotificationDto>>.Success(notificationDto,
                     SuccessCodes.NotificationsReceived);
        }

        public async Task<Result<long>> CountUnReadAsync()
        {
            var count = await _ufw.Notifications
                .CountAsync(n => !n.IsRead && n.RecipientId == _currentUser.AccountId && n.RecipientType == _currentUser.Type!.Value);
            return Result<long>.Success(count,
                          SuccessCodes.CountUnRead);
        }
    }
}
