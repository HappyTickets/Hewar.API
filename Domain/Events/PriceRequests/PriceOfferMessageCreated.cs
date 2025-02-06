using Domain.Entities.ChatAggregate;

namespace Domain.Events.PriceRequests
{
    public class PriceOfferMessageCreated(Message priceOfferMessage, long piceOfferId, long audienceId, EntityTypes audienceType) : DomainEvent
    {
        public Message PiceOfferMessage { get; } = priceOfferMessage;
        public long PiceOfferId { get; } = piceOfferId;
        public long AudienceId { get; } = audienceId;
        public EntityTypes AudienceType { get; } = audienceType;
    }
}
