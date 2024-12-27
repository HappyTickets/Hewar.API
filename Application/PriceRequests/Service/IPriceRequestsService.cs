using Application.PriceRequests.Dtos;

namespace Application.PriceRequests.Service
{
    public interface IPriceRequestsService
    {
        Task<Result<Empty>> AcceptRequestAsync(CreatePriceRequestOfferDto dto);
        Task<Result<long>> CreateRequestAsync(CreatePriceRequestDto dto);
        Task<Result<Empty>> CreateRequestFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto);
        Task<Result<PriceRequestFacilityDetailsDto>> GetRequestFacilityDetailsAsync(long priceRequestId);
        Task<Result<CompanyPriceRequestDto[]>> GetMyRequestsAsCompanyAsync();
        Task<Result<FacilityPriceRequestDto[]>> GetMyRequestsAsFacilityAsync();
        Task<Result<Empty>> RejectRequestAsync(long priceRequestId);
        Task<Result<Empty>> UpdateRequestFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto);
        Task<Result<Empty>> CreateRequestMessageAsync(CreatePriceRequestMessageDto dto);
        Task<Result<PriceRequestMessageDto[]>> GetRequestMessagesAsync(long requestId);
        Task<Result<Empty>> CancelRequestAsync(long priceRequestId);
    }
}
