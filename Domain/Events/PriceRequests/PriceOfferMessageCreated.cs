using Domain.Entities.ChatAggregate;

namespace Domain.Events.PriceRequests
{
    public class PriceOfferMessageCreated : DomainEvent
    {
        public Message PiceOfferMessage { get; }
        public long EntityAudienceId { get; }
        public long EntityIssuerId { get; }

        public PriceOfferMessageCreated(Message priceOfferMessage, long entityAudienceId, long entityIssuerId)
        {
            PiceOfferMessage = priceOfferMessage;
            EntityAudienceId = entityAudienceId;
            EntityIssuerId = entityIssuerId;
        }
    }
}
