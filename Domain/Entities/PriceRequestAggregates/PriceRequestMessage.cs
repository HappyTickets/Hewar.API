namespace Domain.Entities.PriceRequestAggregates
{
    public class PriceRequestMessage : SoftDeletableEntity
    {
        public string Content { get; set; }
        public ICollection<Media> Medias { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public long SenderId { get; set; }
        public AccountTypes SenderType { get; set; }

        public long PriceRequestId { get; set; }

        // nav props
        public PriceRequest PriceRequest { get; set; }
    }
}
