namespace Domain.Common
{
    public class TenantBase: SoftDeletableEntity
    {
        public string? SubscriptionPlan { get; set; }
        public string? CustomAttributes { get; set; }
        public bool IsActive { get; set; } = true;

        // nav props
        public ICollection<Guard> Guards { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Policy> Policies { get; set; }
    }
}
