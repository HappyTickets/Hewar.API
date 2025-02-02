using Application.Ads.Dtos;
using Application.Ads.Dtos.Offers;
using Application.Ads.Dtos.Post;
using Application.Chats.DTOs;

namespace Application.Ads.Service
{
    public interface IAdsService
    {
        Task<Result<Empty>> AcceptOfferAsync(long offerId);
        Task<Result<Empty>> CreateAdAsync(CreateAdDto dto);
        Task<Result<Empty>> CreateOfferAsync(CreateAdOfferDto dto);
        Task<Result<AdDto>> GetAdByIdAsync(long id);
        Task<Result<AdDto[]>> GetMyAdsAsync();
        Task<Result<CompanyAdOfferDto[]>> GetMyOffersAsCompanyAsync();
        Task<Result<CompanyAdOfferDto[]>> GetMyOffersByAdIdAsCompanyAsync(long adId);
        Task<Result<FacilityAdOfferDto[]>> GetMyOffersAsFacilityAsync();
        Task<Result<FacilityAdOfferDto[]>> GetMyOffersByAdIdAsFacilityAsync(long adId);
        Task<Result<AdDto[]>> GetOpenedAdsAsync();
        Task<Result<Empty>> RejectOfferAsync(long offerId);
        Task<Result<Empty>> UpdateAdAsync(UpdateAdDto dto);
        Task<Result<Empty>> CancelOfferAsync(long offerId);

        Task<Result<long>> InitialzeAdOfferChatAsync(long adOfferId);
        Task<Result<Empty>> CreateAdOfferMessageAsync(CreateChatMessageDto dto);
        Task<Result<ChatMessageDto[]>> GetAdOfferChatMessagesAsync(long chatId);

    }
}
