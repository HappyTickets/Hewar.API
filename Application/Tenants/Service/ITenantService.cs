using Application.Tenants.Dtos;

namespace Application.Tenants.Service
{
    public interface ITenantService
    {
        Task<Result<int>> CreateAsync(TenantBriefDto dto);
        Task<Result<TenantBriefDto>> GetByIdAsync(int id);
    }
}
