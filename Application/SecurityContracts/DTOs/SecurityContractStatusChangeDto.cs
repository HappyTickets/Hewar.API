namespace Application.SecurityContracts.DTOs;
public class SecurityContractStatusChangeDto
{
    public long SecurityContractId { get; set; }
    public ContractStatus Status { get; set; }
}
