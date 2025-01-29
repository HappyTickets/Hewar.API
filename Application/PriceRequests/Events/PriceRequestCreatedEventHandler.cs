
using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestCreatedEventHandler : INotificationHandler<PriceRequestCreated>
    {
        private readonly IUnitOfWorkService _ufw;

        public PriceRequestCreatedEventHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

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
                //RecipientType = AccountTypes.Company
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
