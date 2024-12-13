using Application.Notifications.Dtos;
using AutoMapper;
using Domain.Events.Notifications;

namespace Application.Notifications.Events
{
    internal class NotificationCreatedEventHandler : INotificationHandler<NotificationCreated>
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationCreatedEventHandler(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task Handle(NotificationCreated notification, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<NotificationDto>(notification.Notification);
            await _notificationService.NotifyUserAsync(notification.Notification.RecipientId, notification.Notification.RecipientType.ToString(), dto);
        }
    }
}
