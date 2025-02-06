using Domain.Events.Notifications;
using Domain.Events.Tickets;

namespace Application.Chats.Events
{
    internal class TicketMessageCreatedEventHandler(IUnitOfWorkService ufw) : INotificationHandler<TicketMessageCreated>
    {
        public async Task Handle(TicketMessageCreated notification, CancellationToken cancellationToken)
        {
            var userNotification = new Notification
            {
                ContentEn = "You have received a new message on your ticket.",
                ContentAr = "لقد استلمت رسالة جديدة على تذكرتك.",
                IsRead = false,
                ReferenceId = notification.TicketMessage.Ticket.Id,
                ReferenceType = ReferenceTypes.Ticket,
                Event = NotificationEvents.TicketMessageCreated,
                NotifiedOn = DateTimeOffset.UtcNow,
                RecipientId = notification.TicketMessage.Ticket.AudienceId,
                RecipientType = notification.TicketMessage.Ticket.AudienceType
            };

            if (notification.TicketMessage.RepresentedEntity == notification.TicketMessage.Ticket.AudienceType)
            {
                userNotification.RecipientId = notification.TicketMessage.Ticket.IssuerId;
                userNotification.RecipientType = notification.TicketMessage.Ticket.IssuerType;
            }

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            ufw.GetRepository<Notification>().Create(userNotification);
            await ufw.SaveChangesAsync();
        }
    }
}
