using Application.Tenants.Dtos;
using AutoMapper;

namespace Application.Tenants.Mappings
{
    internal class TenantsProfile: Profile
    {
        public TenantsProfile()
        {
            CreateMap<Tenant, TenantBriefDto>();
        }
    }
}
