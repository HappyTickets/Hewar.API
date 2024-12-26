namespace Domain.Events.InsuranceAds
{
    public class InsuranceAdOfferMessageCreated: DomainEvent
    {
        public InsuranceAdOfferMessage InsuranceAdOfferMessage { get; }

        public InsuranceAdOfferMessageCreated(InsuranceAdOfferMessage insuranceAdOfferMessage)
        {
            InsuranceAdOfferMessage = insuranceAdOfferMessage;
        }
    }
}
