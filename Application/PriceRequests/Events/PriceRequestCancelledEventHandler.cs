using Domain.Events.Notifications;
using Domain.Events.PriceRequests;

namespace Application.PriceRequests.Events
{
    internal class PriceRequestCancelledEventHandler : INotificationHandler<PriceRequestCancelled>
    {
        private readonly IUnitOfWorkService _ufw;

        public PriceRequestCancelledEventHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

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
                RecipientType = AccountTypes.Company
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
