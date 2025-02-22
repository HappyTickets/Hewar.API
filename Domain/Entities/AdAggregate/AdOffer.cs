using Domain.Entities.AdAggregate.Services;
using Domain.Entities.ChatAggregate;
using Domain.Entities.CompanyAggregate;

namespace Domain.Entities.InsuranceAdAggregate
{
    public class AdOffer : SoftDeletableEntity
    {
        public RequestStatus Status { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public virtual ICollection<AdHewarServiceCost> ServicesCost { get; set; } = new List<AdHewarServiceCost>();
        public virtual ICollection<OtherAdServiceCost>? OtherServicesCost { get; set; }
        public virtual ICollection<AdCompanyServiceCost>? CompanyServicesCost { get; set; }


        // nav props
        public long AdId { get; set; }
        public virtual Ad Ad { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public long? ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
