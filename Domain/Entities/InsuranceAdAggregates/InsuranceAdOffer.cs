namespace Domain.Entities.InsuranceAdAggregates
{
    public class InsuranceAdOffer : SoftDeletableEntity
    {
        public string Offer { get; set; }
        public RequestStatus Status { get; set; }

        public long InsuranceAdId { get; set; }
        public long CompanyId { get; set; }

        // nav props
        public InsuranceAd InsuranceAd { get; set; }
        public Company Company { get; set; }
        public ICollection<InsuranceAdOfferMessage> InsuranceAdOfferMessages { get; set; }
    }
}
