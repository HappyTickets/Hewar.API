using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ContractTemplate : SoftDeletableEntity
    {

        [Column(TypeName = "nvarchar(max)")]
        public string ContractJson { get; set; }

        public string PartyOneSignature { get; set; } = string.Empty;
        public string PartyTwoSignature { get; set; } = string.Empty;

        public long OfferId { get; set; }


        // navigation properties
        [ForeignKey(nameof(OfferId))]
        public virtual PriceOffer Offer { get; set; }

    }
}
