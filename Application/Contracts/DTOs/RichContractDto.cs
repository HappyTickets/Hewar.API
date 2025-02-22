using Application.Clauses.DTOs;
using Application.Contracts.DTOs.Dynamic;
using Application.Contracts.DTOs.Static;
using Application.PriceOffers.Dtos.Services;
using Application.ScheduleEntries.DTOs;

namespace Application.Contracts.DTOs
{
    public class RichContractDto
    {
        public long ContractId { get; set; }
        public long OfferNumber { get; set; }
        public DateTimeOffset? OfferDate { get; set; }
        public ICollection<GetServiceCostDto> Services { get; set; } = new List<GetServiceCostDto>();
        public ICollection<GetOtherServiceOfferDto>? OtherServices { get; set; }

        public StaticContractDto StaticContractTemplate { get; set; } = new();
        public List<StaticClauseDto> StaticClauses { get; set; } = [new StaticClauseDto { }, new StaticClauseDto { }];
        public List<CustomClauseDto>? CustomClauses { get; set; } = [new CustomClauseDto { }, new CustomClauseDto { }];
        public ICollection<ScheduleEntryDto>? ScheduleEntries { get; set; }
        public List<ContractKeyDto> ContractKeys { get; set; } = [new ContractKeyDto { }, new ContractKeyDto { }];

        public string? FacilitySignature { get; set; }
        public string? CompanySignature { get; set; }

    }
}

