using Domain.Events.InsuranceAds;
using Domain.Events.Notifications;

namespace Application.InsuranceAds.Events
{
    internal class InsuranceAdOfferCreatedHandler : INotificationHandler<InsuranceAdOfferCreated>
    {
        private readonly IUnitOfWorkService _ufw;

        public InsuranceAdOfferCreatedHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(InsuranceAdOfferCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new offer.",
                ContentAr = "لقد استلمت عرض جديد.",
                IsRead = false,
                ReferenceId = notification.InsuranceAdOffer.Id,
                ReferenceType = ReferenceTypes.InsuranceAdOffer,
                Event = NotificationEvents.InsuranceAdOfferCreated,
                CreatedAt = DateTimeOffset.UtcNow,
                RecipientId = notification.InsuranceAdOffer.InsuranceAd.FacilityId,
                RecipientType = AccountTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
