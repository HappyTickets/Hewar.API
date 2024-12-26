namespace Application.InsuranceAds.Dtos
{
    public class CreateInsuranceAdOfferMessageDto
    {
        public string Content { get; set; }
        public IEnumerable<MediaDto>? Medias { get; set; }
        public long InsuranceAdOfferId { get; set; }

    }
}
