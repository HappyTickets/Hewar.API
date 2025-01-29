namespace Domain.Entities.FacilityAggregate
{
    public class Facility : TenantBase
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string CommercialRegistration { get; set; }
        public string ActivityType { get; set; }
        public long AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsiblePhone { get; set; }
        public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}
