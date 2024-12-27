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
                ContentEn = "A new ticket has been created for you.",
                ContentAr = "تم إنشاء تذكرة جديدة لك.",
                IsRead = false,
                ReferenceId = notification.Ticket.Id,
                ReferenceType = ReferenceTypes.Ticket,
                Event = NotificationEvents.TicketCreated,
                CreatedAt = DateTimeOffset.UtcNow,
                RecipientId = notification.Ticket.AudienceId,
                RecipientType = notification.Ticket.AudienceType
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
