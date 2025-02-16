using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ContractAggregate.Dynamic
{
    public class Contract : SoftDeletableEntity
    {
        public ICollection<CustomClause>? CustomClauses { get; set; }
        public ICollection<ContractKey> ContractKeys { get; set; } = new List<ContractKey>();

        public string? FacilitySignature { get; set; }
        public string? CompanySignature { get; set; }

        public long? FacilityId { get; set; }
        public long? CompanyId { get; set; }


        [ForeignKey(nameof(FacilityId))]
        public Facility? Facility { get; set; }


        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        public long? OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public PriceOffer? Offer { get; set; }

    }
}
