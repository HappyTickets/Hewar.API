using Application.SecurityCertificates.DTOs;

namespace Application.SecurityCertificates.Service
{
    public interface ISecurityCertificateService
    {
        Task<Result<long>> CreateAsync(SecurityCertificateCreateDto createDto);
        Task<Result<SecurityCertificateDto>> GetByIdAsync(long id);
        Task<Result<SecurityCertificateDto[]>> GetAllAsync();
        Task<Result<Empty>> DeleteAsync(long id);
        Task<Result<SecurityCertificateDto[]>> GetByFacilityIdAsync(long facilityId);

        Task<Result<Empty>> ApproveAsync(long id);
        Task<Result<Empty>> RejectAsync(long id); Task<Result<Empty>> UpdateAsync(SecurityCertificateUpdateDto updateDto);
    }
}
