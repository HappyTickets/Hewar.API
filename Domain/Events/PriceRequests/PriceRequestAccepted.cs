namespace Domain.Events.PriceRequests
{
    public class PriceRequestAccepted(PriceRequest priceRequest) : DomainEvent
    {
        public PriceRequest PriceRequest { get; } = priceRequest;
    }
}
