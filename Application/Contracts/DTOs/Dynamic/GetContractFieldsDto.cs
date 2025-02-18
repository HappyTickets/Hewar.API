using System.Text.Json.Serialization;

namespace Application.Contracts.DTOs.Dynamic
{
    public class GetContractFieldsDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? OfferId { get; set; }

        public long ContractId { get; set; }

        public ContractFieldsDto ContractFields { get; set; }
    }
}
