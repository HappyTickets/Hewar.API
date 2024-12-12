using Application.Notifications.Dtos;
using AutoMapper;

namespace Application.Notifications.Service
{
    public class NotificationsService: INotificationsService
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
            var notification = await _ufw.Notifications.FirstOrDefaultAsync(n => n.Id == id && n.RecipientId == _currentUser.Id);
            
            if (notification == null)
                return new NotFoundException();

            notification.IsRead = true;
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<IEnumerable<NotificationDto>>> GetAllAsync()
        {
            var notifications = await _ufw.Notifications
                .FilterAsync(n => n.RecipientId == _currentUser.Id);

            return _mapper.Map<NotificationDto[]>(notifications);
        }

        public async Task<Result<long>> CountUnReadAsync()
        {
            return await _ufw.Notifications.CountAsync(n => !n.IsRead && n.RecipientId == _currentUser.Id);
        }
    }
}
