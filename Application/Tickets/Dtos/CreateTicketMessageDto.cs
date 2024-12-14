namespace Application.Tickets.Dtos
{
    public class CreateTicketMessageDto
    {
        public long TicketId { get; set; }
        public string Content { get; set; }
        public IEnumerable<MediaDto>? Medias { get; set; }
    }
}
