using Application.SecurityContracts.DTOs;
using AutoMapper;
using Domain.Entities.FacilityAggregate;

namespace Application.SecurityContracts.Mappings
{
    public class SecurityContractProfile : Profile
    {
        public SecurityContractProfile()
        {
            CreateMap<SecurityContractCreateDto, SecurityContract>();

            CreateMap<SecurityContractUpdateDto, SecurityContract>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<SecurityContract, SecurityContractDto>()
                .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name));

            CreateMap<SecurityContractStatusChangeDto, SecurityContract>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
