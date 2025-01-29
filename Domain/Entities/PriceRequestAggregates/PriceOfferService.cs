using Domain.Entities.CompanyAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PriceRequestAggregates
{
    public class PriceOfferService
    {
        public long Id { get; set; }

        public long PriceOfferId { get; set; }

        [ForeignKey(nameof(PriceOfferId))]
        public virtual PriceOffer PriceOffer { get; set; }
        public long ServiceId { get; set; }
        public virtual CompanyService Service { get; set; }
        public int Quantity { get; set; }
        public decimal CostPerUnit { get; set; }
        public ShiftType ShiftType { get; set; }
    }

}
