using Application.Facilities.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;

namespace Application.Facilities.Mappings
{
    internal class FacilitiesProfile: Profile
    {
        public FacilitiesProfile()
        {
            CreateMap<Facility, FacilityDto>()
                .IncludeMembers(src => src.LoginDetails);

            CreateMap<ApplicationUser, FacilityDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

        }
    }
}
