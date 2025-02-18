namespace Application.Contracts.DTOs.Dynamic
{
    public class UpdateContractKeyDto
    {
        public long ContractKeyId { get; set; }
        public string NewValue { get; set; } = "";
    }
}
