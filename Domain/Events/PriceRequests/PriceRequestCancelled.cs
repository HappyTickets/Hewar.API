namespace Domain.Events.PriceRequests
{
    public class PriceRequestCancelled: DomainEvent
    {
        public PriceRequest PriceRequest { get; }

        public PriceRequestCancelled(PriceRequest priceRequest)
        {
            PriceRequest = priceRequest;
        }
    }
}
