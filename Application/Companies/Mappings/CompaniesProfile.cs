using Application.Companies.Dtos;
using Application.Companies.Dtos.ProvidedServices;
using AutoMapper;
using Domain.Entities.CompanyAggregate;

namespace Application.Companies.Mappings
{
    internal class CompaniesProfile : Profile
    {
        public CompaniesProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();

            CreateMap<Company, CompanyBreifDto>();
            CreateMap<CompanyService, CompanyServiceDto>();
            CreateMap<CreateCompanyServiceDto, CompanyService>();

            CreateMap<ApplicationUser, CompanyBreifDto>();
        }
    }
}
