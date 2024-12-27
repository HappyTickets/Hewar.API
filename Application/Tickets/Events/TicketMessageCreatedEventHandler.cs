using Domain.Events.Notifications;
using Domain.Events.Tickets;

namespace Application.Tickets.Events
{
    internal class TicketMessageCreatedEventHandler : INotificationHandler<TicketMessageCreated>
    {
        private readonly IUnitOfWorkService _ufw;

        public TicketMessageCreatedEventHandler(IUnitOfWorkService ufw)
        {
            _ufw = ufw;
        }

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
                CreatedAt = DateTimeOffset.UtcNow,
                RecipientId = notification.TicketMessage.Ticket.AudienceId,
                RecipientType = notification.TicketMessage.Ticket.AudienceType
            };

            if(notification.TicketMessage.SenderType == notification.TicketMessage.Ticket.AudienceType)
            {
                userNotification.RecipientId = notification.TicketMessage.Ticket.IssuerId;
                userNotification.RecipientType = notification.TicketMessage.Ticket.IssuerType;
            }

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
