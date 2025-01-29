using Domain.Entities.ChatAggregate;
using Domain.Entities.CompanyAggregate;

namespace Domain.Entities.InsuranceAdAggregate
{
    public class AdOffer : SoftDeletableEntity
    {
        public string Offer { get; set; }
        public RequestStatus Status { get; set; }
        public DateTimeOffset SentDate { get; set; }

        public long AdId { get; set; }
        public long CompanyId { get; set; }

        // nav props
        public virtual Ad Ad { get; set; }
        public virtual Company Company { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
