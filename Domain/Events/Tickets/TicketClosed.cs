using Domain.Entities.TicketAggregates;

namespace Domain.Events.Tickets
{
    public class TicketClosed: DomainEvent
    {
        public TicketClosed(Ticket ticket)
        {
            Ticket = ticket;
        }

        public Ticket Ticket { get; }

    }
}
