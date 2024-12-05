namespace Domain.Common
{
    public class SoftDeletableEntity: AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
    }
}
