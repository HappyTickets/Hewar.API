using Application.PriceRequests.Dtos;

namespace Application.PriceRequests.Service
{
    public interface IPriceRequestsService
    {
        Task<Result<long>> CreateRequestAsync(CreatePriceRequestDto dto);
        Task<Result<Empty>> UpdateRequestAsync(UpdatePriceRequestDto dto);
        Task<Result<GetPriceRequestDto>> GetByIdAsync(long priceRequestId);

        #region Facility Details
        //Task<Result<Empty>> CreateRequestFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto);
        //Task<Result<PriceRequestFacilityDetailsDto>> GetRequestFacilityDetailsAsync(long priceRequestId);
        //Task<Result<Empty>> UpdateRequestFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto); 
        #endregion


        Task<Result<CompanyPriceRequestDto[]>> GetMyCompanyRequestsAsync();
        Task<Result<FacilityPriceRequestDto[]>> GetMyFacilityRequestsAsync();


        Task<Result<Empty>> RejectRequestAsync(long priceRequestId);
        Task<Result<Empty>> CancelRequestAsync(long priceRequestId);
        Task<Result<Empty>> HideRequestAsync(long priceRequestId);
        Task<Result<Empty>> ShowRequestAsync(long priceRequestId);
    }
}
