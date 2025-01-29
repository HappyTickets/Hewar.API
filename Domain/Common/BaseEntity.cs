using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public long? TenantId { get; set; }

        private readonly List<DomainEvent> _domainEvents = new();
        [NotMapped]
        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
