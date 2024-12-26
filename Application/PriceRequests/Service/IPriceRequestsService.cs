using Application.PriceRequests.Dtos;

namespace Application.PriceRequests.Service
{
    public interface IPriceRequestsService
    {
        Task<Result<Empty>> AcceptRequestAsync(CreatePriceRequestResponseDto dto);
        Task<Result<long>> CreateRequestAsync(CreatePriceRequestDto dto);
        Task<Result<Empty>> CreateFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto);
        Task<Result<PriceRequestFacilityDetailsDto>> GetFacilityDetailsAsync(long priceRequestId);
        Task<Result<CompanyPriceRequestDto[]>> GetMyRequestsAsCompanyAsync();
        Task<Result<FacilityPriceRequestDto[]>> GetMyRequestsAsFacilityAsync();
        Task<Result<Empty>> RejectRequestAsync(long priceRequestId);
        Task<Result<Empty>> UpdateFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto);
    }
}
