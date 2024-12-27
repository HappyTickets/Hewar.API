using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestMessageCreatedHandler : INotificationHandler<PriceRequestMessageCreated>
    {
        private readonly IUnitOfWorkService _ufw;

        public PriceRequestMessageCreatedHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(PriceRequestMessageCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new message on your price request.",
                ContentAr = "لقد استلمت رسالة جديدة على طلب عرض السعر الخاص بك.",
                IsRead = false,
                ReferenceId = notification.PriceRequestMessage.PriceRequestId,
                ReferenceType = ReferenceTypes.PriceRequest,
                Event = NotificationEvents.PriceRequestMessageCreated,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.PriceRequestMessage.PriceRequest.CompanyId,
                RecipientType = AccountTypes.Company
            };

            if (notification.PriceRequestMessage.SenderType == AccountTypes.Company)
            {
                userNotification.RecipientId = notification.PriceRequestMessage.PriceRequest.FacilityId;
                userNotification.RecipientType = AccountTypes.Facility;
            }

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
