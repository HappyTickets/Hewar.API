namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferAccepted: DomainEvent
    {
        public AdOffer InsuranceAdOffer { get; }

        public InsuranceAdOfferAccepted(AdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
