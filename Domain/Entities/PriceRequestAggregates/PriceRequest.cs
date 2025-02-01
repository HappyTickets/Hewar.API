using Domain.Entities.ChatAggregate;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PriceRequestAggregates
{
    public class PriceRequest : SoftDeletableEntity
    {
        public long FacilityId { get; set; }
        public virtual Facility Facility { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Notes { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public virtual ICollection<PriceRequestService> Services { get; set; } = new List<PriceRequestService>();
        public long? OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public virtual PriceOffer? Offer { get; set; }

        public long? ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
