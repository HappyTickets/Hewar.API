namespace Domain.Entities.InsuranceAdAggregates
{
    public class InsuranceAdOfferMessage : SoftDeletableEntity
    {
        public string Content { get; set; }
        public ICollection<Media> Medias { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long SenderId { get; set; }
        public AccountTypes SenderType { get; set; }

        public long InsuranceAdOfferId { get; set; }

        // nav props
        public InsuranceAdOffer InsuranceAdOffer { get; set; }
    }
}
