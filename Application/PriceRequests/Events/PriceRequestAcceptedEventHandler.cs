using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestAcceptedEventHandler : INotificationHandler<PriceRequestAccepted>
    {
        private readonly IUnitOfWorkService _ufw;

        public PriceRequestAcceptedEventHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(PriceRequestAccepted notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your price request has been accepted.",
                ContentAr = "تم قبول طلب عرض السعر الخاص بك.",
                IsRead = false,
                ReferenceId = notification.PriceRequest.Id,
                ReferenceType = ReferenceTypes.PriceRequest,
                Event = NotificationEvents.PriceRequestAccepted,
                CreatedAt = DateTimeOffset.UtcNow,
                RecipientId = notification.PriceRequest.FacilityId,
                RecipientType = AccountTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
