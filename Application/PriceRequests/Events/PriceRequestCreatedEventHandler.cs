
using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestCreatedEventHandler(IUnitOfWorkService ufw) : INotificationHandler<PriceRequestCreated>
    {
        public async Task Handle(PriceRequestCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new price request.",
                ContentAr = "لقد استلمت طلب عرض سعر جديد.",
                IsRead = false,
                ReferenceId = notification.PriceRequest.Id,
                ReferenceType = ReferenceTypes.PriceRequest,
                Event = NotificationEvents.PriceRequestCreated,
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
