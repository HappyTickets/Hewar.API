namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferCreated : DomainEvent
    {
        public AdOffer InsuranceAdOffer { get; }

        public InsuranceAdOfferCreated(AdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
