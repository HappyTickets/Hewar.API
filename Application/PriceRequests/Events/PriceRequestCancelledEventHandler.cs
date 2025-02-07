using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestCancelledEventHandler(IUnitOfWorkService ufw) : INotificationHandler<PriceRequestCancelled>
    {
        public async Task Handle(PriceRequestCancelled notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your price request has been cancelled.",
                ContentAr = "تم الغاء طلب عرض السعر الخاص بك.",
                IsRead = false,
                ReferenceId = notification.PriceRequest.Id,
                ReferenceType = ReferenceTypes.PriceRequest,
                Event = NotificationEvents.PriceRequestCancelled,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.PriceRequest.CompanyId,
                RecipientType = EntityTypes.Company
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            ufw.GetRepository<Notification>().Create(userNotification);
            await ufw.SaveChangesAsync();
        }
    }
}
