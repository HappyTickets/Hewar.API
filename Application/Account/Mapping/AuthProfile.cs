using Application.AccountManagement.Dtos.User;
using AutoMapper;

namespace Application.AccountManagement.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();

        }
    }
}
