using Application.Guards.Dtos;
using AutoMapper;

namespace Application.Guards.Mappings
{
    public class GuardsProfile : Profile
    {
        public GuardsProfile()
        {
            CreateMap<Guard, GuardDto>();

            CreateMap<ApplicationUser, GuardDto>()
                  .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<Skill, SkillDto>()
                .ReverseMap();

            CreateMap<PrevCompany, PrevCompanyDto>()
                .ReverseMap();
        }
    }
}
