using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestAcceptedEventHandler(IUnitOfWorkService ufw) : INotificationHandler<PriceRequestAccepted>
    {
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
