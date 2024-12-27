using Domain.Entities.PriceRequestAggregates;

namespace Domain.Events.PriceRequests
{
    public class PriceRequestCreated:DomainEvent
    {
        public PriceRequest PriceRequest { get; }

        public PriceRequestCreated(PriceRequest priceRequest)
        {
            PriceRequest = priceRequest;
        }
    }
}
