namespace Domain.Events.Ads
{
    public class AdOfferCreated : DomainEvent
    {
        public AdOffer AdOffer { get; }

        public AdOfferCreated(AdOffer insuranceAdOffer)
        {
            AdOffer = insuranceAdOffer;
        }
    }
}
