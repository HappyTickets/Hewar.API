namespace Domain.Events.Tickets
{
    public class TicketMessageCreated : DomainEvent
    {
        public TicketMessage TicketMessage { get; }

        public TicketMessageCreated(TicketMessage ticketMessage)
        {
            TicketMessage = ticketMessage;
        }
    }
}
