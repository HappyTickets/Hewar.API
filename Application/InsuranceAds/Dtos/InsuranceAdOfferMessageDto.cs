namespace Application.InsuranceAds.Dtos
{
    public class InsuranceAdOfferMessageDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public ICollection<MediaDto> Medias { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long SenderId { get; set; }
    }
}
