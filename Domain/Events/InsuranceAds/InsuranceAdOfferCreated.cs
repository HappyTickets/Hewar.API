namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferCreated : DomainEvent
    {
        public InsuranceAdOffer InsuranceAdOffer { get; }

        public InsuranceAdOfferCreated(InsuranceAdOffer insuranceAdOffer)
        {
            InsuranceAdOffer = insuranceAdOffer;
        }
    }
}
