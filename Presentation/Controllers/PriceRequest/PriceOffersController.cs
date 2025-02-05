using Application.PriceOffers.Dtos;
using Application.PriceOffers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.PriceRequest
{
    public class PriceOffersController(IPriceOfferService prService) : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateOfferAsync(CreatePriceOfferDto dto)
          => Result(await prService.CreateOfferAsync(dto));

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOfferAsync(UpdatePriceOfferDto dto)
            => Result(await prService.UpdateOfferAsync(dto));

        [HttpPatch("accept")]
        public async Task<IActionResult> AcceptOfferAsync(long offerId)
            => Result(await prService.AcceptOfferAsync(offerId));

        [HttpPatch("reject")]
        public async Task<IActionResult> RejectOfferAsync(long offerId)
            => Result(await prService.RejectOfferAsync(offerId));

        [HttpPatch("cancel")]
        public async Task<IActionResult> CancelOfferAsync(long offerId)
            => Result(await prService.CancelOfferAsync(offerId));

        [HttpGet("getMyCompanyOffers")]
        public async Task<IActionResult> GetMyCompanyOffersAsync()
            => Result(await prService.GetMyCompanyOffersAsync());


        [HttpGet("getMyCompanyOffersByRequestId")]
        public async Task<IActionResult> GetMyCompanyOffersByRequestIdAsync(long requestId)
             => Result(await prService.GetMyCompanyOffersByRequestIdAsync(requestId));

        [HttpGet("getMyFacilityOffers")]
        public async Task<IActionResult> GetMyFacilityOffersAsync()
            => Result(await prService.GetMyFacilityOffersAsync());

        [HttpGet("getMyFacilityOffersByRequestId")]
        public async Task<IActionResult> GetMyFacilityOffersByRequestIdAsync(long requestId)
            => Result(await prService.GetMyFacilityOffersByRequestIdAsync(requestId));
    }

}
