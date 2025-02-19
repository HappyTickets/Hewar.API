using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ContractAggregate.Dynamic
{
    public class ScheduleEntry : SoftDeletableEntity
    {
        public long ContractId { get; set; }

        [ForeignKey(nameof(ContractId))]
        public Contract Contract { get; set; }

        public string LocationAr { get; set; }
        public string LocationEn { get; set; }
        public int GuardsRequired { get; set; }

        public string ShiftTimeAr { get; set; }
        public string ShiftTimeEn { get; set; }

        public string? NotesAr { get; set; }
        public string? NotesEn { get; set; }
    }
}
