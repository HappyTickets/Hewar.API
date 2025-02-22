using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.AdAggregate.Services
{
    public class OtherAdServiceCost
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }
        public ShiftType ShiftType { get; set; }

        public long AdOfferId { get; set; }

        [ForeignKey(nameof(AdOfferId))]
        public virtual AdOffer AdOffer { get; set; }
    }
}
