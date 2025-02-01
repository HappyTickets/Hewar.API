using Domain.Entities.ChatAggregate;

namespace Domain.Events.PriceRequests
{
    public class PriceRequestMessageCreated : DomainEvent
    {
        public Message PriceRequestMessage { get; }
        public long EntityAudienceId { get; }
        public long EntityIssuerId { get; }

        public PriceRequestMessageCreated(Message priceRequestMessage, long entityAudienceId, long entityIssuerId)
        {
            PriceRequestMessage = priceRequestMessage;
            EntityAudienceId = entityAudienceId;
            EntityIssuerId = entityIssuerId;
        }
    }
}
