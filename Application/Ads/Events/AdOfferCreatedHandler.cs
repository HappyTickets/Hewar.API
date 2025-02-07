using Domain.Events.Ads;
using Domain.Events.Notifications;

namespace Application.Ads.Events
{
    internal class AdOfferCreatedHandler(IUnitOfWorkService ufw) : INotificationHandler<AdOfferCreated>
    {
        public async Task Handle(AdOfferCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new offer.",
                ContentAr = "لقد استلمت عرض جديد.",
                IsRead = false,
                ReferenceId = notification.AdOffer.Id,
                ReferenceType = ReferenceTypes.AdOffer,
                Event = NotificationEvents.AdOfferCreated,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.AdOffer.Ad.FacilityId,
                RecipientType = EntityTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            ufw.GetRepository<Notification>().Create(userNotification);
            await ufw.SaveChangesAsync();
        }
    }
}
