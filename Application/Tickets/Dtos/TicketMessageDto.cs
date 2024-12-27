namespace Application.Tickets.Dtos
{
    public class TicketMessageDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<MediaDto> Medias { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long SenderId { get; set; }
        public AccountTypes SenderType { get; set; }
    }
}
