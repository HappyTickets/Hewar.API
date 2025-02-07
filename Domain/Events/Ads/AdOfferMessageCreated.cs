using Domain.Entities.ChatAggregate;

namespace Domain.Events.Ads
{
    public class AdOfferMessageCreated(Message adOfferMessage, long adOfferId, long audienceId, EntityTypes audienceType) : DomainEvent
    {
        public long AdOfferId { get; } = adOfferId;
        public Message AdOfferMessage { get; } = adOfferMessage;
        public long AudienceId { get; } = audienceId;
        public EntityTypes AudienceType { get; } = audienceType;
    }
}
