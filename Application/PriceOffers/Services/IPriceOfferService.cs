using Application.PriceOffers.Dtos;

namespace Application.PriceOffers.Services
{
    public interface IPriceOfferService
    {
        Task<Result<long>> CreateOfferAsync(CreatePriceOfferDto dto);
        Task<Result<Empty>> UpdateOfferAsync(UpdatePriceOfferDto dto);

        Task<Result<GetCompanyPriceOfferDetailedDto>> GetByIdAsync(long offerId);

        Task<Result<Empty>> AcceptOfferAsync(long offerId);
        Task<Result<Empty>> RejectOfferAsync(long offerId);
        Task<Result<Empty>> CancelOfferAsync(long offerId);
        Task<Result<Empty>> ShowOfferAsync(long priceOfferId);
        Task<Result<Empty>> HideOfferAsync(long priceOfferId);

        Task<Result<GetCompanyPriceOfferDetailedDto[]>> GetMyCompanyOffersAsync();

        Task<Result<GetOffersForRequest>> GetMyCompanyOffersByRequestIdAsync(long requestId);

        Task<Result<GetFacilityPriceOfferDetailedDto[]>> GetMyFacilityOffersAsync();
        Task<Result<GetOffersForRequest>> GetMyFacilityOffersByRequestIdAsync(long requestId);


    }
}
