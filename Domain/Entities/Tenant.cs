namespace Domain.Entities
{
    public class Tenant : SoftDeletableEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string SubscriptionPlan { get; set; }
        public bool IsActive { get; set; }
        public string CustomAttributes { get; set; }
        
        // nav props
        public ICollection<Guard> Guards { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Policy> Policies { get; set; }
    }
}
