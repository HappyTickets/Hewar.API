using Application.Notifications.Dtos;
using AutoMapper;
using Domain.Events.Notifications;

namespace Application.Notifications.Events
{
    internal class NotificationCreatedEventHandler(INotificationService notificationService, IMapper mapper) : INotificationHandler<NotificationCreated>
    {
        public async Task Handle(NotificationCreated notification, CancellationToken cancellationToken)
        {
            var dto = mapper.Map<NotificationDto>(notification.Notification);

            await notificationService.NotifyUserAsync(notification.Notification.RecipientId, notification.Notification.RecipientType, dto);
        }
    }
}
