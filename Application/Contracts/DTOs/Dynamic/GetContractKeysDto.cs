namespace Application.Contracts.DTOs.Dynamic
{
    public class GetContractKeysDto
    {
        public long ContractId { get; set; }
        public List<ContractKeyDto> ContractKeys { get; set; } = [new ContractKeyDto(), new ContractKeyDto()];
    }
}
