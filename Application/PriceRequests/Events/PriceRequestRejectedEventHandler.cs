using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestRejectedEventHandler(IUnitOfWorkService ufw) : INotificationHandler<PriceRequestRejected>
    {
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
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.PriceRequest.FacilityId,
                RecipientType = EntityTypes.Facility
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            ufw.GetRepository<Notification>().Create(userNotification);
            await ufw.SaveChangesAsync();
        }
    }
}
