using Domain.Events.Notifications;
using Domain.Events.Tickets;

namespace Application.Tickets.Events
{
    internal class TicketClosedEventHandler(IUnitOfWorkService ufw) : INotificationHandler<TicketClosed>
    {
        public async Task Handle(TicketClosed notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "Your ticket has been closed.",
                ContentAr = "تم إغلاق التذكرة الخاصة بك.",
                IsRead = false,
                ReferenceId = notification.Ticket.Id,
                ReferenceType = ReferenceTypes.Ticket,
                Event = NotificationEvents.TicketClosed,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.Ticket.IssuerId,
                RecipientType = notification.Ticket.IssuerType
            };

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            ufw.GetRepository<Notification>().Create(userNotification);
            await ufw.SaveChangesAsync();
        }
    }
}
