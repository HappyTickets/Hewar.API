using Domain.Entities.AdAggregate;
using Domain.Entities.ChatAggregate;
using Domain.Entities.CompanyAggregate;

namespace Domain.Entities.InsuranceAdAggregate
{
    public class AdOffer : SoftDeletableEntity
    {
        public RequestStatus Status { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public virtual ICollection<AdServicePrice> ServicesPrice { get; set; }
            = new List<AdServicePrice>();


        // nav props
        public long AdId { get; set; }
        public virtual Ad Ad { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public long? ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
