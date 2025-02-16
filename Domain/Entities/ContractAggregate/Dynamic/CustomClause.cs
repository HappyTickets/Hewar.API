using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ContractAggregate.Dynamic
{
    public class CustomClause : SoftDeletableEntity
    {
        public string HtmlContentAr { get; set; }
        public string HtmlContentEn { get; set; }

        public long ContractId { get; set; }

        [ForeignKey(nameof(ContractId))]
        public Contract Contract { get; set; }

    }
}
