using Domain.Events.Ads;
using Domain.Events.Notifications;

namespace Application.Ads.Events
{
    internal class AdOfferCancelledEventHandler : INotificationHandler<AdOfferCancelled>
    {
        private readonly IUnitOfWorkService _ufw;

        public AdOfferCancelledEventHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(AdOfferCancelled notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your offer has been cancelled.",
                ContentAr = "تم الغاء العرض الخاصة بك.",
                IsRead = false,
                ReferenceId = notification.AdOffer.Id,
                ReferenceType = ReferenceTypes.AdOffer,
                Event = NotificationEvents.AdOfferCancelled,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.AdOffer.Ad.FacilityId,
                //RecipientType = AccountTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.GetRepository<Notification>().Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
