namespace Domain.Common
{
    public abstract class SoftDeletableEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
    }
}
