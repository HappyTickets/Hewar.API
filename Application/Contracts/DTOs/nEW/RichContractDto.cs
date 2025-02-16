namespace Application.Contracts.DTOs.nEW
{
    public class RichContractDto
    {
        public long ContractId { get; set; }
        public StaticContractTemplateDto StaticContractTemplate { get; set; } = new();
        public List<StaticClauseDto> StaticClauses { get; set; } = [new StaticClauseDto { }, new StaticClauseDto { }];
        public List<CustomClauseDto>? CustomClauses { get; set; } = [new CustomClauseDto { }, new CustomClauseDto { }];
        public List<ContractKeyDto> ContractKeys { get; set; } = [new ContractKeyDto { }, new ContractKeyDto { }];

        public string? FacilitySignature { get; set; }
        public string? CompanySignature { get; set; }

    }
}

