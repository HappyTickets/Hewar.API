using Application.Tenants.Dtos;

namespace Application.Tenants.Service
{
    public interface ITenantService
    {
        Task<Result<long>> CreateAsync(TenantBriefDto dto);
        Task<Result<TenantBriefDto>> GetByIdAsync(long id);
    }
}
