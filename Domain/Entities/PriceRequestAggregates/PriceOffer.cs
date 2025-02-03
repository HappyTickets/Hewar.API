using Domain.Entities.ChatAggregate;

namespace Domain.Entities.PriceRequestAggregates
{
    public class PriceOffer : SoftDeletableEntity
    {
        public long PriceRequestId { get; set; }
        public virtual PriceRequest PriceRequest { get; set; }
        public RequestStatus OfferStatus { get; set; }

        public virtual ICollection<ServiceOffer> Services { get; set; } = new List<ServiceOffer>();
        public virtual ICollection<OtherServiceOffer>? OtherServices { get; set; }

        public long? ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
