using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.Chats.Events
{
    internal class PriceRequestMessageCreatedHandler(IUnitOfWorkService ufw) : INotificationHandler<PriceRequestMessageCreated>
    {
        public async Task Handle(PriceRequestMessageCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new message on your price request.",
                ContentAr = "لقد استلمت رسالة جديدة على طلب عرض السعر الخاص بك.",
                IsRead = false,
                ReferenceId = notification.PriceRequestId,
                ReferenceType = ReferenceTypes.PriceRequest,
                Event = NotificationEvents.PriceRequestMessageCreated,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.AudienceId,
                RecipientType = notification.AudienceType
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            ufw.GetRepository<Notification>().Create(userNotification);
            await ufw.SaveChangesAsync();
        }
    }
}
