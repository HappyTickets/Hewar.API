namespace Application.PriceRequests.Dtos
{
    public class CreatePriceRequestMessageDto
    {
        public string Content { get; set; }
        public IEnumerable<MediaDto>? Medias { get; set; }
        public long PriceRequestId { get; set; }
    }
}
