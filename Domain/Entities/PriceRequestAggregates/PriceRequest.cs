using Domain.Entities.ChatAggregate;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;

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
        public string? Notes { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public virtual ICollection<ServiceRequest> Services { get; set; } = new List<ServiceRequest>();
        public virtual ICollection<OtherRequestedService>? OtherServices { get; set; }

        public virtual ICollection<PriceOffer> Offers { get; set; } = new List<PriceOffer>();

        public long? ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
