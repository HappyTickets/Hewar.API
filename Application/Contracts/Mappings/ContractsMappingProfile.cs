using Application.Contracts.DTOs;
using Application.Contracts.DTOs.Dynamic;
using AutoMapper;

namespace Application.Contracts.Mappings
{
    public class ContractMappingProfile : Profile
    {
        public ContractMappingProfile()
        {
            CreateMap<ContractFields1, GetContractFieldsDto>()
                .ForMember(dest => dest.ContractId, opt => opt.Ignore());
        }
    }

}
