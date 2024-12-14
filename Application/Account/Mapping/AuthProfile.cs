using Application.AccountManagement.Dtos.User;
using AutoMapper;
using Domain.Entities.UserEntities;

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
