using Application.PriceRequests.Dtos.Chat;
using Application.PriceRequests.Dtos.Offers;
using Application.PriceRequests.Dtos.Requests;

namespace Application.PriceRequests.Service
{
    public interface IPriceRequestsService
    {
        Task<Result<long>> CreateRequestAsync(CreatePriceRequestDto dto);
        Task<Result<Empty>> AcceptRequestAsync(CreatePriceOfferDto dto);

        #region Facility Details
        //Task<Result<Empty>> CreateRequestFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto);
        //Task<Result<PriceRequestFacilityDetailsDto>> GetRequestFacilityDetailsAsync(long priceRequestId);
        //Task<Result<Empty>> UpdateRequestFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto); 
        #endregion

        Task<Result<Empty>> CreateRequestMessageAsync(CreateChatMessageDto dto);

        Task<Result<CompanyPriceRequestDto[]>> GetMyRequestsAsCompanyAsync();
        Task<Result<FacilityPriceRequestDto[]>> GetMyRequestsAsFacilityAsync();

        Task<Result<long>> InitialzePriceRequestChatAsync(long priceRequestId);
        Task<Result<long>> InitialzeOfferChatAsync(long priceRequestId);

        Task<Result<ChatMessageDto[]>> GetChatMessagesAsync(long chatId);

        Task<Result<Empty>> RejectRequestAsync(long priceRequestId);
        Task<Result<Empty>> CancelRequestAsync(long priceRequestId);
    }
}
