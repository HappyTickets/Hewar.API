namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferCancelled: DomainEvent
    {
        public AdOffer InsuranceAdOffer { get; }

        public InsuranceAdOfferCancelled(AdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
