using Application.PriceOffers.Dtos;
using Application.PriceOffers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.PriceRequest
{
    public class PriceOffersController(IPriceOfferService prService) : ApiControllerBase
    {
        [HttpPost("offers")]
        public async Task<IActionResult> CreateOfferAsync(CreatePriceOfferDto dto)
          => Result(await prService.CreateOfferAsync(dto));

        [HttpPut("offers")]
        public async Task<IActionResult> UpdateOfferAsync(UpdatePriceOfferDto dto)
            => Result(await prService.UpdateOfferAsync(dto));

        [HttpPatch("offers/{offerId}/accept")]
        public async Task<IActionResult> AcceptOfferAsync(long offerId)
            => Result(await prService.AcceptOfferAsync(offerId));

        [HttpPatch("offers/{offerId}/reject")]
        public async Task<IActionResult> RejectOfferAsync(long offerId)
            => Result(await prService.RejectOfferAsync(offerId));

        [HttpPatch("offers/{offerId}/cancel")]
        public async Task<IActionResult> CancelOfferAsync(long offerId)
            => Result(await prService.CancelOfferAsync(offerId));

        [HttpGet("company/offers")]
        public async Task<IActionResult> GetMyCompanyOffersAsync()
            => Result(await prService.GetMyCompanyOffersAsync());


        [HttpGet("company/offers/request/{requestId:long}")]
        public async Task<IActionResult> GetCompanyOffersByRequestIdAsync(long requestId)
             => Result(await prService.GetCompanyOffersByRequestIdAsync(requestId));

        [HttpGet("facility/offers")]
        public async Task<IActionResult> GetFacilityOffersAsync()
            => Result(await prService.GetFacilityOffersAsync());

        [HttpGet("facility/offers/request/{requestId:long}")]
        public async Task<IActionResult> GetFacilityOffersByRequestIdAsync(long requestId)
            => Result(await prService.GetFacilityOffersByRequestIdAsync(requestId));


        #region Chat

        //[HttpPatch("initializeOfferChat")]
        ////[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        //public async Task<IActionResult> InitialzeOfferChatAsync(long offerId)
        //  => Result(await prService.InitialzeOfferChatAsync(offerId));

        //[HttpGet("getChatMessages")]
        ////[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        //public async Task<IActionResult> GetChatMessagesAsync(long chatId)
        //    => Result(await prService.GetChatMessagesAsync(chatId));

        #endregion
    }

}
