using Domain.Entities.ChatAggregate;

namespace Domain.Events.Ads
{
    public class AdOfferMessageCreated(Message adOfferMessage, long issuerId, long audienceId) : DomainEvent
    {
        public Message AdOfferMessage { get; } = adOfferMessage;
        public long IssuerId { get; } = issuerId;
        public long AudienceId { get; } = audienceId;
    }
}
