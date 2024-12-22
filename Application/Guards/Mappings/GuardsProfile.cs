using Application.Guards.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;
using LanguageExt;

namespace Application.Guards.Mappings
{
    public class GuardsProfile: Profile
    {
        public GuardsProfile()
        {
            CreateMap<Guard, GuardDto>()
                .IncludeMembers(src => src.LoginDetails);

            CreateMap<ApplicationUser, GuardDto>()
                  .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
