using Domain.Events.Ads;
using Domain.Events.Notifications;

namespace Application.Ads.Events
{
    internal class AdOfferRejectedHandler : INotificationHandler<AdOfferRejected>
    {
        private readonly IUnitOfWorkService _ufw;

        public AdOfferRejectedHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

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
                //RecipientType = AccountTypes.Company
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.GetRepository<Notification>().Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
