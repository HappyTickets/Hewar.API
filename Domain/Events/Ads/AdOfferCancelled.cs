namespace Domain.Events.Ads
{
    public class AdOfferCancelled: DomainEvent
    {
        public AdOffer AdOffer { get; }

        public AdOfferCancelled(AdOffer insuranceAdOffer)
        {
            AdOffer = insuranceAdOffer;
        }
    }
}
