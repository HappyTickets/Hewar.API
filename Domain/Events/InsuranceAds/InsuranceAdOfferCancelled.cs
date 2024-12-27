namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferCancelled: DomainEvent
    {
        public InsuranceAdOffer InsuranceAdOffer { get; }

        public InsuranceAdOfferCancelled(InsuranceAdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
