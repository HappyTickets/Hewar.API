using Domain.Entities.PriceRequestAggregates;

namespace Domain.Events.PriceRequests
{
    public class PriceRequestRejected: DomainEvent
    {
        public PriceRequest PriceRequest { get; }

        public PriceRequestRejected(PriceRequest priceRequest)
        {
            PriceRequest = priceRequest;
        }
    }
}
