namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferRejected : DomainEvent
    {
        public AdOffer InsuranceAdOffer { get; }

        public InsuranceAdOfferRejected(AdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
