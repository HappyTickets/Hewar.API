using Domain.Events.Ads;
using Domain.Events.Notifications;

namespace Application.Ads.Events
{
    internal class AdOfferAcceptedHandler : INotificationHandler<AdOfferAccepted>
    {
        private readonly IUnitOfWorkService _ufw;

        public AdOfferAcceptedHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(AdOfferAccepted notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your offer has been accepted.",
                ContentAr = "تم قبول العرض الخاصة بك.",
                IsRead = false,
                ReferenceId = notification.AdOffer.Id,
                ReferenceType = ReferenceTypes.AdOffer,
                Event = NotificationEvents.AdOfferAccepted,
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
