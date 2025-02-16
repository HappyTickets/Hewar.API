namespace Application.Contracts.DTOs.nEW
{
    public class GetContractKeysDto
    {
        public long ContractId { get; set; }
        public List<ContractKeyDto> ContractKeys { get; set; } = [new ContractKeyDto(), new ContractKeyDto()];
    }
}
