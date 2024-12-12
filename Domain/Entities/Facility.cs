using Domain.Entities.UserEntities;

namespace Domain.Entities
{
    public class Facility: TenantBase
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string CommercialRegistration { get; set; }
        public string ActivityType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsiblePhone { get; set; }
        public long LoginDetailsId { get; set; }

        // nav props
        public ApplicationUser LoginDetails { get; set; }
        public ICollection<PriceRequest> PriceRequests { get; set; }
    }
}
