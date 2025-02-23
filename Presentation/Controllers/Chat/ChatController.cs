using Application.Chats.DTOs;
using Application.Chats.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Chat
{
    [Authorize]
    [AnyEntityType(EntityTypes.Facility, EntityTypes.Company)]
    public class ChatController(IChatService chatService) : ApiControllerBase
    {


        [HttpPatch("initializePriceRequestChat")]
        [HasPermission(Permissions.CreatePriceRequestChat)]
        public async Task<IActionResult> InitialzePriceRequestChatAsync(long priceRequestId)
            => Result(await chatService.InitialzePriceRequestChatAsync(priceRequestId));

        [HttpPatch("initializePriceOfferChat")]
        [HasPermission(Permissions.CreatePriceOfferChat)]
        public async Task<IActionResult> InitialzePriceOfferChatAsync(long priceOfferId)
            => Result(await chatService.InitialzePriceOfferChatAsync(priceOfferId));

        [HttpPatch("initializeAdOfferChat")]
        [HasPermission(Permissions.CreateAdOfferChat)]
        public async Task<IActionResult> InitialzeAdOfferChatAsync(long adOfferId)
            => Result(await chatService.InitialzeAdOfferChatAsync(adOfferId));

        [HttpPost("sendPriceRequestMessage")]
        [HasPermission(Permissions.SendPriceRequestMessage)]
        public async Task<IActionResult> SendPriceRequestMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendPriceRequestMessageAsync(dto));

        [HttpPost("sendPriceOfferMessage")]
        [HasPermission(Permissions.SendPriceOfferMessage)]
        public async Task<IActionResult> SendPriceOfferMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendPriceOfferMessageAsync(dto));

        [HttpPost("sendAdOfferMessage")]
        [HasPermission(Permissions.SendAdOfferMessage)]
        public async Task<IActionResult> SendAdOfferMessageAsync([FromBody] CreateChatMessageDto dto)
            => Result(await chatService.SendAdOfferMessageAsync(dto));

        [HttpGet("getMessagesByChatId")]
        [HasPermission(Permissions.ViewChatMessages)]
        public async Task<IActionResult> GetChatMessagesAsync(long chatId)
            => Result(await chatService.GetChatMessagesAsync(chatId));
    }
}
