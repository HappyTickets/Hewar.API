using Domain.Entities.CompanyAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PriceRequestAggregates
{
    public class ServiceOffer
    {
        public long Id { get; set; }

        public long PriceOfferId { get; set; }

        [ForeignKey(nameof(PriceOfferId))]
        public virtual PriceOffer PriceOffer { get; set; }
        public long ServiceId { get; set; }
        public virtual CompanyService Service { get; set; }
        public int Quantity { get; set; }
        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }
        public ShiftType ShiftType { get; set; }
    }

}
