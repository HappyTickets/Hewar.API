using Domain.Events.Ads;
using Domain.Events.Notifications;

namespace Application.Ads.Events
{
    internal class AdOfferCreatedHandler : INotificationHandler<AdOfferCreated>
    {
        private readonly IUnitOfWorkService _ufw;

        public AdOfferCreatedHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(AdOfferCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new offer.",
                ContentAr = "لقد استلمت عرض جديد.",
                IsRead = false,
                ReferenceId = notification.InsuranceAdOffer.Id,
                ReferenceType = ReferenceTypes.AdOffer,
                Event = NotificationEvents.AdOfferCreated,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.InsuranceAdOffer.Ad.FacilityId,
                //RecipientType = AccountTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.GetRepository<Notification>().Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
