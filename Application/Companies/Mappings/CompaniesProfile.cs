using Application.Companies.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;

namespace Application.Companies.Mappings
{
    internal class CompaniesProfile: Profile
    {
        public CompaniesProfile()
        {
            CreateMap<Company, CompanyDto>()
                .IncludeMembers(src => src.LoginDetails);

            CreateMap<ApplicationUser, CompanyDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

        }
    }
}
