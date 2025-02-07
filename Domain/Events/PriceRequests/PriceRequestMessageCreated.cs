using Domain.Entities.ChatAggregate;

namespace Domain.Events.PriceRequests
{
    public class PriceRequestMessageCreated(Message priceRequestMessage, long priceRequestId, long audienceId, EntityTypes audienceType) : DomainEvent
    {
        public Message PriceRequestMessage { get; } = priceRequestMessage;
        public long PriceRequestId { get; } = priceRequestId;
        public long AudienceId { get; } = audienceId;
        public EntityTypes AudienceType { get; } = audienceType;
    }
}
