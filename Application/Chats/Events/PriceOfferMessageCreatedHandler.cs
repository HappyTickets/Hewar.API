using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.Chats.Events
{
    internal class PriceOfferMessageCreatedHandler(IUnitOfWorkService ufw) : INotificationHandler<PriceOfferMessageCreated>
    {
        public async Task Handle(PriceOfferMessageCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new message on your price offer.",
                ContentAr = "لقد استلمت رسالة جديدة على عرض السعر الخاص بك.",
                IsRead = false,
                ReferenceId = notification.PiceOfferId,
                ReferenceType = ReferenceTypes.PriceOffer,
                Event = NotificationEvents.PriceOfferMessageCreated,
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
