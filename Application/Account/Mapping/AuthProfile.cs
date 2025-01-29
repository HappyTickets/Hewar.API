using Application.Account.Dtos.User;
using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.User;
using AutoMapper;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;

namespace Application.AccountManagement.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, AdminInfoDto>()
                        .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                        .ReverseMap();

            CreateMap<RegisterGuardRequest, Guard>()
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));

            CreateMap<RegisterFacilityRequest, Facility>();

            CreateMap<RegisterCompanyRequest, Company>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
           .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail));
        }
    }
}
