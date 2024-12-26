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
                RecipientId = notification.TicketMessage.Ticket.PriceRequest.FacilityId,
                RecipientType = AccountTypes.Facility
            };

            if(notification.TicketMessage.SenderType == AccountTypes.Facility)
            {
                userNotification.RecipientId = notification.TicketMessage.Ticket.PriceRequest.CompanyId;
                userNotification.RecipientType = AccountTypes.Company;
            }

            userNotification.AddDomainEvent(new NotificationCreated(userNotification));
            _ufw.Notifications.Create(userNotification);
            await _ufw.SaveChangesAsync();
        }
    }
}
