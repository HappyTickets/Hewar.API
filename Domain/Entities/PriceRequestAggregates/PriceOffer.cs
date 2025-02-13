using Domain.Common.Interfaces;
using Domain.Entities.ChatAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PriceRequestAggregates
{
    public class PriceOffer : SoftDeletableEntity, IToggleableEntity
    {
        public long PriceRequestId { get; set; }
        [ForeignKey(nameof(PriceRequestId))]
        public virtual PriceRequest PriceRequest { get; set; }
        public RequestStatus OfferStatus { get; set; }


        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }



        public bool IsFacilityHidden { get; set; } = false;
        public bool IsCompanyHidden { get; set; } = false;

        public virtual ICollection<ServiceOffer> Services { get; set; } = new List<ServiceOffer>();
        public virtual ICollection<OtherServiceOffer>? OtherServices { get; set; }

        public long? ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
