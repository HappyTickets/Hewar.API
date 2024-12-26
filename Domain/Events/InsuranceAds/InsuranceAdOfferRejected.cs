namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferRejected : DomainEvent
    {
        public InsuranceAdOffer InsuranceAdOffer { get; }

        public InsuranceAdOfferRejected(InsuranceAdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
