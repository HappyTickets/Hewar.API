using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ContractTemplate : SoftDeletableEntity
    {
        [NotMapped]
        public ContractJson.ContractJson ContractTemplateObject
        {
            get => string.IsNullOrWhiteSpace(ContractJson) ? null : JsonConvert.DeserializeObject<ContractJson.ContractJson>(ContractJson);
            set => ContractJson = JsonConvert.SerializeObject(value);
        }

        [Column(TypeName = "nvarchar(max)")]
        public string ContractJson { get; set; }

        public long OfferId { get; set; }


        // navigation properties
        public virtual PriceOffer Offer { get; set; }

    }
}
