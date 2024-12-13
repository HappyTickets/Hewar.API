using Domain.Entities.Owned;

namespace Domain.Entities
{
    public class TicketMessage : SoftDeletableEntity
    {
        public long TicketId { get; set; }
        public string Content { get; set; }
        public ICollection<Media> Medias { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long SenderId { get; set; }
        public SenderTypes SenderType { get; set; }

        // nav props
        public Ticket Ticket { get; set; }
    }
}
