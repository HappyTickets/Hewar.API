using Domain.Entities.ChatAggregate;

namespace Domain.Entities.PriceRequestAggregates
{
    public class PriceOffer : SoftDeletableEntity
    {
        public long PriceRequestId { get; set; }
        public virtual PriceRequest PriceRequest { get; set; }
        public RequestStatus OfferStatus { get; set; }

        public virtual ICollection<PriceOfferService> Services { get; set; } = new List<PriceOfferService>();

        public long? ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
