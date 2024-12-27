using Application.InsuranceAds.Dtos;

namespace Application.InsuranceAds.Service
{
    public interface IInsuranceAdsService
    {
        Task<Result<Empty>> AcceptOfferAsync(long offerId);
        Task<Result<Empty>> CreateAdAsync(CreateInsuranceAdDto dto);
        Task<Result<Empty>> CreateOfferMessageAsync(CreateInsuranceAdOfferMessageDto dto);
        Task<Result<Empty>> CreateOfferAsync(CreateInsuranceAdOfferDto dto);
        Task<Result<InsuranceAdDto>> GetAdByIdAsync(long id);
        Task<Result<InsuranceAdOfferMessageDto[]>> GetOfferMessagesAsync(long offerId);
        Task<Result<InsuranceAdDto[]>> GetMyAdsAsync();
        Task<Result<CompanyInsuranceAdOfferDto[]>> GetMyOffersAsCompanyAsync();
        Task<Result<CompanyInsuranceAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId);
        Task<Result<FacilityInsuranceAdOfferDto[]>> GetMyOffersAsFacilityAsync();
        Task<Result<FacilityInsuranceAdOfferDto[]>> GetMyOffersByAdIdAsFacilityAsync(long adId);
        Task<Result<InsuranceAdDto[]>> GetOpenedAdsAsync();
        Task<Result<Empty>> RejectOfferAsync(long offerId);
        Task<Result<Empty>> UpdateAdAsync(UpdateInsuranceAdDto dto);
        Task<Result<Empty>> CancelOfferAsync(long offerId);
    }
}
