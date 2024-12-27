namespace Domain.Entities.TicketAggregates
{
    public class TicketMessage : SoftDeletableEntity
    {
        public string Content { get; set; }
        public ICollection<Media> Medias { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long SenderId { get; set; }
        public AccountTypes SenderType { get; set; }
        public long TicketId { get; set; }


        // nav props
        public Ticket Ticket { get; set; }
    }
}
