using Application.Chats.DTOs;

namespace Application.Chats.Service
{
    public interface IChatService
    {
        Task<Result<Empty>> SendPriceRequestMessageAsync(CreateChatMessageDto dto);
        Task<Result<Empty>> SendPriceOfferMessageAsync(CreateChatMessageDto dto);
        Task<Result<long>> InitialzePriceRequestChatAsync(long priceRequestId);
        Task<Result<long>> InitialzePriceOfferChatAsync(long offerId);
        Task<Result<ChatMessageDto[]>> GetChatMessagesAsync(long chatId);


        #region Ads Chat
        Task<Result<long>> InitialzeAdOfferChatAsync(long adOfferId);
        Task<Result<Empty>> SendAdOfferMessageAsync(CreateChatMessageDto dto);

        #endregion


    }
}
