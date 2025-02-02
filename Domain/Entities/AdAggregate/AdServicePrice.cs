using Domain.Entities.CompanyAggregate;

namespace Domain.Entities.AdAggregate
{
    public class AdServicePrice
    {
        public long Id { get; set; }
        public long ServiceId { get; set; }
        public virtual CompanyService Service { get; set; }
        public int Quantity { get; set; }
        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }
        public ShiftType ShiftType { get; set; }

        public long AdOfferId { get; set; }
        public virtual AdOffer AdOffer { get; set; }
    }
}
