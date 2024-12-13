namespace Domain.Events.PriceRequests
{
    internal class PriceRequestAccepted: DomainEvent
    {
        public PriceRequest PriceRequest { get; }

        public PriceRequestAccepted(PriceRequest priceRequest)
        {
            PriceRequest = priceRequest;
        }
    }
}
