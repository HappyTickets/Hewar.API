using Application.SecurityContracts.DTOs;

namespace Application.SecurityContracts.Service
{
    public interface ISecurityContractService
    {
        Task<Result<long>> CreateAsync(SecurityContractCreateDto createDto);
        Task<Result<SecurityContractDto>> GetByIdAsync(long id);
        Task<Result<Empty>> DeleteAsync(long id);
        Task<Result<SecurityContractDto[]>> GetByFacilityIdAsync(long facilityId);

        Task<Result<Empty>> ApproveAsync(long id);
        Task<Result<Empty>> RejectAsync(long id); Task<Result<Empty>> UpdateAsync(SecurityContractUpdateDto updateDto);
    }
}
