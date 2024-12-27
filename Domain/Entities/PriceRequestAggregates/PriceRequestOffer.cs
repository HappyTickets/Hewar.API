namespace Domain.Entities.PriceRequestAggregates
{
    public class PriceRequestOffer : SoftDeletableEntity
    {
        public string Offer { get; set; }
        public DateTimeOffset RespondedDate { get; set; }
        public long PriceRequestId { get; set; }

        // nav props
        public PriceRequest PriceRequest { get; set; }
    }
}
