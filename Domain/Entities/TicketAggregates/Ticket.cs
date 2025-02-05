namespace Domain.Entities.TicketAggregates
{
    public class Ticket : SoftDeletableEntity
    {
        public string Title { get; set; }
        public DateTimeOffset OpenedDate { get; set; }
        public DateTimeOffset? ClosedDate { get; set; }
        public TicketStatus Status { get; set; }
        public long AudienceId { get; set; }
        public EntityTypes AudienceType { get; set; }
        public long IssuerId { get; set; }
        public EntityTypes IssuerType { get; set; }

        // nav props
        public ICollection<TicketMessage> Messages { get; set; }

    }
}
