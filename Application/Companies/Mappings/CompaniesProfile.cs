using Application.Companies.Dtos;
using AutoMapper;
using Domain.Entities.CompanyAggregate;

namespace Application.Companies.Mappings
{
    internal class CompaniesProfile : Profile
    {
        public CompaniesProfile()
        {
            CreateMap<Company, CompanyDto>();

            CreateMap<ApplicationUser, CompanyDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<Company, CompanyBreifDto>();

            CreateMap<ApplicationUser, CompanyBreifDto>();
        }
    }
}
