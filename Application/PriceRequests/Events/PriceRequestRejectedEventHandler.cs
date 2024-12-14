using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestRejectedEventHandler : INotificationHandler<PriceRequestRejected>
    {
        private readonly IUnitOfWorkService _ufw;

        public PriceRequestRejectedEventHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

        public async Task Handle(PriceRequestRejected notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your price request has been rejected.",
                ContentAr = "تم رفض طلب عرض السعر الخاص بك.",
                IsRead = false,
                ReferenceId = notification.PriceRequest.Id,
                ReferenceType = ReferenceTypes.PriceRequest,
                Event = NotificationEvents.PriceRequestRejected,
                CreatedAt = DateTimeOffset.UtcNow,
                RecipientId = notification.PriceRequest.FacilityId,
                RecipientType = RecipientTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
