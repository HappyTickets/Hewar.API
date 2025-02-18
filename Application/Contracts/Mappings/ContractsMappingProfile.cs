using Application.Contracts.DTOs.Dynamic;
using Application.Contracts.DTOs.Static;
using AutoMapper;
using Domain.Entities.ContractAggregate.Dynamic;
using Domain.Entities.ContractAggregate.Static;

namespace Application.Contracts.Mappings
{
    public class ContractMappingProfile : Profile
    {
        public ContractMappingProfile()
        {
            CreateMap<StaticContract, StaticContractDto>()
                .ReverseMap();

            CreateMap<ContractKey, GetContractKeysDto>()

                .ReverseMap();
            CreateMap<ContractKey, ContractKeyDto>()
                .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.Key.DataType))
                .ReverseMap();
        }
    }

}
