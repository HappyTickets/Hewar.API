namespace Domain.Events.Ads
{
    public class AdOfferCreated : DomainEvent
    {
        public AdOffer InsuranceAdOffer { get; }

        public AdOfferCreated(AdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
