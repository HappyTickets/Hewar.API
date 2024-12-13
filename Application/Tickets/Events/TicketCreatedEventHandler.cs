using Domain.Events.Notifications;
using Domain.Events.Tickets;

namespace Application.Tickets.Events
{
    internal class TicketCreatedEventHandler : INotificationHandler<TicketCreated>
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly ICurrentUserService _currentUser;

        public TicketCreatedEventHandler(IUnitOfWorkService ufw, ICurrentUserService currentUser)
        {
            _ufw = ufw;
            _currentUser = currentUser;
        }

        public async Task Handle(TicketCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "A new ticket has been created for your price request.",
                ContentAr = "تم إنشاء تذكرة جديدة لطلب عرض السعر الخاص بك.",
                IsRead = false,
                ReferenceId = notification.Ticket.Id,
                ReferenceType = ReferenceTypes.Ticket,
                Event = NotificationEvents.TicketCreated,
                CreatedAt = DateTimeOffset.UtcNow,
                RecipientId = notification.Ticket.PriceRequest.CompanyId,
                RecipientType = RecipientTypes.Company
            };

            if(_currentUser.Role == Roles.Company.ToString())
            {
                userNotification.RecipientId = notification.Ticket.PriceRequest.FacilityId;
                userNotification.RecipientType = RecipientTypes.Facility;
            }

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
