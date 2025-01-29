namespace Domain.Events.Ads
{
    public class AdOfferAccepted: DomainEvent
    {
        public AdOffer AdOffer { get; }

        public AdOfferAccepted(AdOffer insuranceAdOffer)
        {
            AdOffer = insuranceAdOffer;
        }
    }
}
