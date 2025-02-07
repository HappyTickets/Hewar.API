using Domain.Events.Notifications;
using Domain.Events.Tickets;

namespace Application.Tickets.Events
{
    internal class TicketCreatedEventHandler(IUnitOfWorkService ufw, ICurrentUserService currentUser) : INotificationHandler<TicketCreated>
    {
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
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.Ticket.AudienceId,
                RecipientType = notification.Ticket.AudienceType
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            ufw.GetRepository<Notification>().Create(userNotification);
            await ufw.SaveChangesAsync();
        }
    }
}
