using Domain.Entities.MissionAggregate;

namespace Domain.Entities.CompanyAggregate
{
    public class Company : TenantBase
    {
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxId { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Logo { get; set; }
        public long AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<AdOffer> InsuranceAdOffers { get; set; } = new List<AdOffer>();
        public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();
        public virtual ICollection<CompanyService> Services { get; set; } = new List<CompanyService>();
    }
}
