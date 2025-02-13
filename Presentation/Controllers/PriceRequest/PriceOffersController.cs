using Application.PriceOffers.Dtos;
using Application.PriceOffers.Services;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.PriceRequest
{
    public class PriceOffersController(IPriceOfferService offerService) : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateOfferAsync(CreatePriceOfferDto dto)
          => Result(await offerService.CreateOfferAsync(dto));

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOfferAsync(UpdatePriceOfferDto dto)
            => Result(await offerService.UpdateOfferAsync(dto));

        [HttpGet("getById")]
        public async Task<IActionResult> GetOfferByIdAsync(long offerId)
            => Result(await offerService.GetByIdAsync(offerId));




        [HttpPatch("accept")]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        public async Task<IActionResult> AcceptOfferAsync(long offerId)
            => Result(await offerService.AcceptOfferAsync(offerId));

        [HttpPatch("reject")]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        public async Task<IActionResult> RejectOfferAsync(long offerId)
            => Result(await offerService.RejectOfferAsync(offerId));

        [HttpPatch("cancel")]
        public async Task<IActionResult> CancelOfferAsync(long offerId)
            => Result(await offerService.CancelOfferAsync(offerId));

        [HttpPatch("hide")]
        //[HasAccountType(AccountTypes.Facility)] or company 
        public async Task<IActionResult> HidePriceOfferAsync(long priceOfferId)
             => Result(await offerService.HideOfferAsync(priceOfferId));

        [HttpPatch("show")]
        //[HasAccountType(AccountTypes.Facility)] or company 
        public async Task<IActionResult> ShowPriceOfferAsync(long priceOfferId)
            => Result(await offerService.ShowOfferAsync(priceOfferId));



        [HttpGet("getMyCompanyOffers")]
        public async Task<IActionResult> GetMyCompanyOffersAsync()
            => Result(await offerService.GetMyCompanyOffersAsync());


        [HttpGet("getMyCompanyOffersByRequestId")]
        public async Task<IActionResult> GetMyCompanyOffersByRequestIdAsync(long requestId)
             => Result(await offerService.GetMyCompanyOffersByRequestIdAsync(requestId));

        [HttpGet("getMyFacilityOffers")]
        public async Task<IActionResult> GetMyFacilityOffersAsync()
            => Result(await offerService.GetMyFacilityOffersAsync());

        [HttpGet("getMyFacilityOffersByRequestId")]
        public async Task<IActionResult> GetMyFacilityOffersByRequestIdAsync(long requestId)
            => Result(await offerService.GetMyFacilityOffersByRequestIdAsync(requestId));
    }

}
