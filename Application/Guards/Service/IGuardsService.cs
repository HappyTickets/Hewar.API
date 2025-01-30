using Application.AccountManagement.Dtos.Authentication;
using Application.Guards.Dtos;

namespace Application.Guards.Service
{
    public interface IGuardsService
    {
        Task<Result<Empty>> CreateAsync(RegisterGuardRequest registerRequest, CancellationToken cancellationToken = default);
        Task<Result<GuardDto[]>> GetAllAsync();
        Task<Result<GuardDto>> GetByIdAsync(long id);
        Task<Result<Empty>> DeleteAsync(long id);
        Task<Result<Empty>> UpdateAsync(UpdateGuardDto dto);
    }
}
