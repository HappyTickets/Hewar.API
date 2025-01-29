namespace Application.Ads.Dtos
{
    public class CreateAdOfferMessageDto
    {
        public string Content { get; set; }
        public IEnumerable<MediaDto>? Medias { get; set; }
        public long AdOfferId { get; set; }

    }
}
