using Application.PriceOffers.Dtos;

namespace Application.PriceOffers.Services
{
    public interface IPriceOfferService
    {
        Task<Result<long>> CreateOfferAsync(CreatePriceOfferDto dto);
        Task<Result<Empty>> UpdateOfferAsync(UpdatePriceOfferDto dto);

        Task<Result<Empty>> AcceptOfferAsync(long offerId);
        Task<Result<Empty>> RejectOfferAsync(long offerId);
        Task<Result<Empty>> CancelOfferAsync(long offerId);
        Task<Result<GetPriceOfferDetailedDto[]>> GetMyCompanyOffersAsync();

        Task<Result<GetOffersForRequest>> GetCompanyOffersByRequestIdAsync(long requestId);

        Task<Result<GetPriceOfferDetailedDto[]>> GetFacilityOffersAsync();
        Task<Result<GetOffersForRequest>> GetFacilityOffersByRequestIdAsync(long requestId);


    }
}
