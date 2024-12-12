namespace Domain.Entities
{
    public class PriceRequestResponse: SoftDeletableEntity
    {
        public string Content { get; set; }
        public DateTimeOffset RespondedDate { get; set; }
        public long PriceRequestId { get; set; }

        // nav props
        public PriceRequest PriceRequest { get; set; }
    }
}
