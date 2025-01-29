using Application.Facilities.Dtos;
using AutoMapper;
using Domain.Entities.FacilityAggregate;

namespace Application.Facilities.Mappings
{
    internal class FacilitiesProfile : Profile
    {
        public FacilitiesProfile()
        {
            CreateMap<Facility, FacilityDto>();

            CreateMap<ApplicationUser, FacilityDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<Facility, FacilityBreifDto>();
            CreateMap<ApplicationUser, FacilityBreifDto>();
        }
    }
}
