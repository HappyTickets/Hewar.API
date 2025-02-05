using Application.PriceOffers.Dtos;

namespace Application.PriceOffers.Services
{
    public interface IPriceOfferService
    {
        Task<Result<long>> CreateOfferAsync(CreatePriceOfferDto dto);
        Task<Result<Empty>> UpdateOfferAsync(UpdatePriceOfferDto dto);

        Task<Result<GetPriceOfferDetailedDto>> GetByIdAsync(long offerId);

        Task<Result<Empty>> AcceptOfferAsync(long offerId);
        Task<Result<Empty>> RejectOfferAsync(long offerId);
        Task<Result<Empty>> CancelOfferAsync(long offerId);
        Task<Result<GetPriceOfferDetailedDto[]>> GetMyCompanyOffersAsync();

        Task<Result<GetOffersForRequest>> GetMyCompanyOffersByRequestIdAsync(long requestId);

        Task<Result<GetPriceOfferDetailedDto[]>> GetMyFacilityOffersAsync();
        Task<Result<GetOffersForRequest>> GetMyFacilityOffersByRequestIdAsync(long requestId);


    }
}
