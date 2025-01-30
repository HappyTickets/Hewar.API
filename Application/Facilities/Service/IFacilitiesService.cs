using Application.AccountManagement.Dtos.Authentication;
using Application.Facilities.Dtos;

namespace Application.Facilities.Service
{
    public interface IFacilitiesService
    {
        Task<Result<Empty>> CreateAsync(RegisterFacilityRequest registerRequest, CancellationToken cancellationToken = default);
        Task<Result<FacilityDto[]>> GetAllAsync();
        Task<Result<FacilityDto>> GetByIdAsync(long id);
        Task<Result<Empty>> HardDeleteAsync(long id);
        Task<Result<Empty>> SoftDeleteAsync(long id);
        Task<Result<Empty>> UpdateAsync(UpdateFacilityDto dto);
    }
}
