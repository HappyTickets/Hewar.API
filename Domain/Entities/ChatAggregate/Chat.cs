namespace Domain.Entities.ChatAggregate
{
    public class Chat : SoftDeletableEntity
    {
        public long RelatedEntityId { get; set; }
        public ChatEntityType RelatedEntityType { get; set; } // PriceRequest  PriceOffer  AdOffer 

        public long EntityIssuerId { get; set; }
        public long EntityAudienceId { get; set; }

        public ChatStatus Status { get; set; } = ChatStatus.Open;
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }

}