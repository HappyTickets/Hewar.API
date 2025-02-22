using Domain.Entities.AdAggregate.Services;
using Domain.Entities.FacilityAggregate;

namespace Domain.Entities.InsuranceAdAggregate
{
    public class Ad : SoftDeletableEntity
    {
        public string Title { get; set; }

        public DateTimeOffset DatePosted { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public AdStatus Status { get; set; }
        public ContractType ContractType { get; set; }
        public long FacilityId { get; set; }
        public virtual Facility Facility { get; set; }

        public virtual ICollection<AdHewarService> Services { get; set; } = new List<AdHewarService>();
        public virtual ICollection<OtherAdService>? OtherServices { get; set; }
        public virtual ICollection<AdOffer>? AdOffers { get; set; }
    }
}
