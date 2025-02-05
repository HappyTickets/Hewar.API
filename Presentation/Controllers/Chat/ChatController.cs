using Application.Chats.DTOs;
using Application.Chats.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Chat
{
    public class ChatController(IChatService chatService) : ApiControllerBase
    {

        [HttpPatch("initializePriceRequestChat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzePriceRequestChatAsync(long priceRequestId)
    => Result(await chatService.InitialzePriceRequestChatAsync(priceRequestId));

        [HttpPatch("initializePriceOfferChat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzePriceOfferChatAsync(long priceOfferId)
            => Result(await chatService.InitialzePriceOfferChatAsync(priceOfferId));

        [HttpPatch("initializeAdOfferChat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzeAdOfferChatAsync(long adOfferId)
            => Result(await chatService.InitialzeAdOfferChatAsync(adOfferId));



        [HttpPost("sendPriceRequestMessage")]
        public async Task<IActionResult> SendPriceRequestMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendPriceRequestMessageAsync(dto));

        [HttpPost("sendPriceOfferMessage")]
        public async Task<IActionResult> SendPriceOfferMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendPriceOfferMessageAsync(dto));

        [HttpPost("sendAdOfferMessage")]
        public async Task<IActionResult> SendAdOfferMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendAdOfferMessageAsync(dto));

        [HttpGet("getMessagesByChatId")]
        public async Task<IActionResult> GetChatMessagesAsync(long chatId)
            => Result(await chatService.GetChatMessagesAsync(chatId));
    }
}
