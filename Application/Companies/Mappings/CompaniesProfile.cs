using Application.Companies.Dtos;
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


            CreateMap<ApplicationUser, CompanyBreifDto>();
        }
    }
}
