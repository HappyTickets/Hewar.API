using Domain.Events.InsuranceAds;
using Domain.Events.Notifications;

namespace Application.InsuranceAds.Events
{
    internal class InsuranceAdOfferAcceptedHandler : INotificationHandler<InsuranceAdOfferAccepted>
    {
        private readonly IUnitOfWorkService _ufw;

        public InsuranceAdOfferAcceptedHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(InsuranceAdOfferAccepted notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your offer has been accepted.",
                ContentAr = "تم قبول العرض الخاصة بك.",
                IsRead = false,
                ReferenceId = notification.InsuranceAdOffer.Id,
                ReferenceType = ReferenceTypes.InsuranceAdOffer,
                Event = NotificationEvents.InsuranceAdOfferAccepted,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.InsuranceAdOffer.CompanyId,
                //RecipientType = AccountTypes.Company
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
