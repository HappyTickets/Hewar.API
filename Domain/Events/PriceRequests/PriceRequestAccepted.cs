namespace Domain.Events.PriceRequests
{
    public class PriceRequestAccepted : DomainEvent
    {
        public PriceRequest PriceRequest { get; }

        public PriceRequestAccepted(PriceRequest priceRequest)
        {
            PriceRequest = priceRequest;
        }
    }
}
