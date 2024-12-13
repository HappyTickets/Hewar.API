namespace Domain.Events.Tickets
{
    public class TicketCreated: DomainEvent
    {
        public Ticket Ticket { get; }

        public TicketCreated(Ticket ticket)
        {
            Ticket = ticket;
        }
    }
}
