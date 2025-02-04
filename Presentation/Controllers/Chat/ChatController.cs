using Application.Chats.DTOs;
using Application.Chats.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Chat
{
    public class ChatController(IChatService chatService) : ApiControllerBase
    {
        #region Price Requests/Offers
        [HttpPost("messages/priceRequest")]
        public async Task<IActionResult> SendPriceRequestMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendPriceRequestMessageAsync(dto));

        [HttpPost("messages/priceOffer")]
        public async Task<IActionResult> SendPriceOfferMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendPriceOfferMessageAsync(dto));

        [HttpGet("{chatId}/messages")]
        public async Task<IActionResult> GetChatMessagesAsync(long chatId)
            => Result(await chatService.GetChatMessagesAsync(chatId));

        [HttpPatch("requests/{priceRequestId}/initialize-chat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzePriceRequestChatAsync(long priceRequestId)
            => Result(await chatService.InitialzePriceRequestChatAsync(priceRequestId));

        [HttpPatch("offers/{priceOfferId}/initialize-chat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzePriceOfferChatAsync(long priceOfferId)
            => Result(await chatService.InitialzePriceOfferChatAsync(priceOfferId));
        #endregion

        #region Ads Chat
        [HttpPatch("ads/offers/{adOfferId}/initialize-chat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzeAdOfferChatAsync(long adOfferId)
            => Result(await chatService.InitialzeAdOfferChatAsync(adOfferId));

        [HttpPost("messages/adOffer")]
        public async Task<IActionResult> SendAdOfferMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendAdOfferMessageAsync(dto));
        #endregion
    }
}
