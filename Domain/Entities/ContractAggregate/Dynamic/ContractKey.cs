using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ContractAggregate.Dynamic
{
    public class ContractKey : SoftDeletableEntity
    {
        public long ContractId { get; set; }
        public long KeyId { get; set; }
        public string Value { get; set; }


        [ForeignKey(nameof(ContractKey))]
        public Contract Contract { get; set; }

        [ForeignKey(nameof(KeyId))]
        public Key Key { get; set; }

    }
}