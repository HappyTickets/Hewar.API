using Domain.Events.Ads;
using Domain.Events.Notifications;

namespace Application.Chats.Events
{
    internal class InsuranceAdOfferMessageCreatedHandler(IUnitOfWorkService ufw) : INotificationHandler<AdOfferMessageCreated>
    {
        public async Task Handle(AdOfferMessageCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new message on your offer.",
                ContentAr = "لقد استلمت رسالة جديدة على عرضك.",
                IsRead = false,
                ReferenceId = notification.AdOfferMessage.ChatId,
                ReferenceType = ReferenceTypes.Chat,
                Event = NotificationEvents.AdOfferMessageCreated,
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
