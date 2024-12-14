namespace Application.Tickets.Dtos
{
    public class CreateTicketDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<MediaDto>? Medias { get; set; }
        public long PriceRequestId { get; set; }
    }
}
