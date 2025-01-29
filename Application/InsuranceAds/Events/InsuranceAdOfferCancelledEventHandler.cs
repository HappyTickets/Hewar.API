using Domain.Events.InsuranceAds;
using Domain.Events.Notifications;

namespace Application.InsuranceAds.Events
{
    internal class InsuranceAdOfferCancelledEventHandler : INotificationHandler<InsuranceAdOfferCancelled>
    {
        private readonly IUnitOfWorkService _ufw;

        public InsuranceAdOfferCancelledEventHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(InsuranceAdOfferCancelled notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your offer has been cancelled.",
                ContentAr = "تم الغاء العرض الخاصة بك.",
                IsRead = false,
                ReferenceId = notification.InsuranceAdOffer.Id,
                ReferenceType = ReferenceTypes.InsuranceAdOffer,
                Event = NotificationEvents.InsuranceAdOfferCancelled,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.InsuranceAdOffer.Ad.FacilityId,
                //RecipientType = AccountTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
