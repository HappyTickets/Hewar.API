using Application.Guards.Dtos;

namespace Application.Guards.Service
{
    public interface IGuardsService
    {
        Task<Result<Empty>> CreateAsync(CreateGuardDto dto);
        Task<Result<GuardDto[]>> GetAllAsync();
        Task<Result<GuardDto>> GetByIdAsync(long id);
        Task<Result<Empty>> HardDeleteAsync(long id);
        Task<Result<Empty>> SoftDeleteAsync(long id);
        Task<Result<Empty>> UpdateAsync(UpdateGuardDto dto);
    }
}
