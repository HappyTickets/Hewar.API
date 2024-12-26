using Domain.Entities.PriceRequestAggregates;

namespace Domain.Entities
{
    public class Ticket : SoftDeletableEntity
    {
        public string Title { get; set; }
        public DateTimeOffset OpenedDate { get; set; }
        public DateTimeOffset? ClosedDate { get; set; } // Closed date, nullable
        public TicketStatus Status { get; set; }
        public long PriceRequestId { get; set; }

        // nav props
        public PriceRequest PriceRequest { get; set; }
        public ICollection<TicketMessage> Messages { get; set; }
    }
}
