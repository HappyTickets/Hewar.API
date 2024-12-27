using Domain.Entities.PriceRequestAggregates;

namespace Domain.Entities.TicketAggregates
{
    public class Ticket : SoftDeletableEntity
    {
        public string Title { get; set; }
        public DateTimeOffset OpenedDate { get; set; }
        public DateTimeOffset? ClosedDate { get; set; } // Closed date, nullable
        public TicketStatus Status { get; set; }
        public long AudienceId { get; set; }
        public AccountTypes AudienceType { get; set; }
        public long IssuerId { get; set; }
        public AccountTypes IssuerType { get; set; }

        // nav props
        public ICollection<TicketMessage> Messages { get; set; }
    }
}
