using Domain.Entities.UserEntities;

namespace Domain.Entities
{
    public class Company: TenantBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long LoginDetailsId { get; set; }

        // nav props
        public ApplicationUser LoginDetails { get; set; }
        public ICollection<PriceRequest> PriceRequests { get; set; }

    }
}
