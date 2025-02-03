namespace Domain.Entities.CompanyAggregate
{
    public class CompanyService : SoftDeletableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
