using Application.Authorization.DTOs.Response;
using AutoMapper;
using Domain.Entities.UserEntities;

namespace Application.Authorization.Mapping
{


    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<ApplicationRole, RoleDto>()
                .ForMember(r => r.Permissions, opt => opt.MapFrom(r => r.Permissions!.Select(p=>p.Permission)));

        }
    }
}
