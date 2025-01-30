using Application.Facilities.Dtos;
using AutoMapper;
using Domain.Entities.FacilityAggregate;

namespace Application.Facilities.Mappings
{
    internal class FacilitiesProfile : Profile
    {
        public FacilitiesProfile()
        {
            CreateMap<Facility, FacilityDto>()
                .ForMember(x => x.Address, s => s.MapFrom(src => src.Address));

            CreateMap<Facility, FacilityBreifDto>();
        }
    }
}
