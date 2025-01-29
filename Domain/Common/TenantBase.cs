namespace Domain.Common
{
    public abstract class TenantBase : SoftDeletableEntity
    {
        public long ParentTenantId { get; set; }
        public string? SubscriptionPlan { get; set; }
        public string? CustomAttributes { get; set; }
        public bool IsActive { get; set; } = true;
        public Cities City { get; set; }

        // Navigation properties
        public ICollection<Guard> Guards { get; set; } = new List<Guard>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
        public ICollection<PriceRequest> PriceRequests { get; set; } = new List<PriceRequest>();
    }

}
