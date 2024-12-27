using Domain.Entities.PriceRequestAggregates;

namespace Domain.Entities
{
    public class Company: TenantBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public long LoginDetailsId { get; set; }

        // nav props
        public ApplicationUser LoginDetails { get; set; }
        public ICollection<PriceRequest> PriceRequests { get; set; }
        public ICollection<InsuranceAdOffer> InsuranceAdOffers { get; set; }

    }
}
