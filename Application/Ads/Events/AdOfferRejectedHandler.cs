using Domain.Events.Ads;
using Domain.Events.Notifications;

namespace Application.Ads.Events
{
    internal class AdOfferRejectedHandler(IUnitOfWorkService ufw) : INotificationHandler<AdOfferRejected>
    {
        public async Task Handle(AdOfferRejected notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your offer has been rejected.",
                ContentAr = "تم رفض العرض الخاصة بك.",
                IsRead = false,
                ReferenceId = notification.AdOffer.Id,
                ReferenceType = ReferenceTypes.AdOffer,
                Event = NotificationEvents.AdOfferRejected,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.AdOffer.CompanyId,
                RecipientType = EntityTypes.Company
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            ufw.GetRepository<Notification>().Create(userNotification);
            await ufw.SaveChangesAsync();
        }
    }
}
