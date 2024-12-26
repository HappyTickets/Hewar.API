namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferAccepted: DomainEvent
    {
        public InsuranceAdOffer InsuranceAdOffer { get; }

        public InsuranceAdOfferAccepted(InsuranceAdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
