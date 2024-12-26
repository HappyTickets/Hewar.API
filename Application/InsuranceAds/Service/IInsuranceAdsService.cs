using Application.InsuranceAds.Dtos;

namespace Application.InsuranceAds.Service
{
    public interface IInsuranceAdsService
    {
        Task<Result<Empty>> CreateAdAsync(CreateInsuranceAdDto dto);
        Task<Result<InsuranceAdDto>> GetAdByIdAsync(long id);
        Task<Result<InsuranceAdDto[]>> GetMyAdsAsync();
        Task<Result<InsuranceAdDto[]>> GetOpenedAdsAsync();
        Task<Result<Empty>> UpdateAdAsync(UpdateInsuranceAdDto dto);
    }
}
