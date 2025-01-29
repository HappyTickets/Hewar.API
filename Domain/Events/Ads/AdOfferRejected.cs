namespace Domain.Events.Ads
{
    public class AdOfferRejected : DomainEvent
    {
        public AdOffer AdOffer { get; }

        public AdOfferRejected(AdOffer insuranceAdOffer)
        {
            AdOffer = insuranceAdOffer;
        }
    }
}
