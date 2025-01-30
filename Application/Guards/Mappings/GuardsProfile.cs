using Application.Guards.Dtos;
using AutoMapper;

namespace Application.Guards.Mappings
{
    public class GuardsProfile : Profile
    {
        public GuardsProfile()
        {
            CreateMap<Guard, GuardDto>();

            CreateMap<Skill, SkillDto>()
                .ReverseMap();

            CreateMap<UpdateGuardDto, Guard>();



            CreateMap<PrevCompany, PrevCompanyDto>()
                    .ReverseMap();
        }
    }
}
