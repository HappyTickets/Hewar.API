using Application.Ads.Dtos;

namespace Application.Ads.Service
{
    public interface IAdsService
    {
        Task<Result<Empty>> AcceptOfferAsync(long offerId);
        Task<Result<Empty>> CreateAdAsync(CreateAdDto dto);
        Task<Result<Empty>> CreateOfferMessageAsync(CreateAdOfferMessageDto dto);
        Task<Result<Empty>> CreateOfferAsync(CreateAdOfferDto dto);
        Task<Result<AdDto>> GetAdByIdAsync(long id);
        Task<Result<AdOfferMessageDto[]>> GetOfferMessagesAsync(long offerId);
        Task<Result<AdDto[]>> GetMyAdsAsync();
        Task<Result<CompanyAdOfferDto[]>> GetMyOffersAsCompanyAsync();
        Task<Result<CompanyAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId);
        Task<Result<FacilityAdOfferDto[]>> GetMyOffersAsFacilityAsync();
        Task<Result<FacilityAdOfferDto[]>> GetMyOffersByAdIdAsFacilityAsync(long adId);
        Task<Result<AdDto[]>> GetOpenedAdsAsync();
        Task<Result<Empty>> RejectOfferAsync(long offerId);
        Task<Result<Empty>> UpdateAdAsync(UpdateAdDto dto);
        Task<Result<Empty>> CancelOfferAsync(long offerId);
    }
}
