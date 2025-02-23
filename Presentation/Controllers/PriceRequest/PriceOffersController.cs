using Application.PriceOffers.Dtos;
using Application.PriceOffers.Services;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.PriceRequest
{
    [Authorize]
    public class PriceOffersController(IPriceOfferService offerService) : ApiControllerBase
    {
        [HttpPost("create")]
        [HasPermission(Permissions.CreatePriceOffer)]
        public async Task<IActionResult> CreateOfferAsync(CreatePriceOfferDto dto)
          => Result(await offerService.CreateOfferAsync(dto));

        [HttpPut("update")]
        [HasPermission(Permissions.UpdatePriceOffer)]
        public async Task<IActionResult> UpdateOfferAsync(UpdatePriceOfferDto dto)
            => Result(await offerService.UpdateOfferAsync(dto));

        [HttpGet("getById")]
        [HasPermission(Permissions.ViewPriceOffers)]
        public async Task<IActionResult> GetOfferByIdAsync(long offerId)
            => Result(await offerService.GetByIdAsync(offerId));



        [HttpPatch("accept")]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        [AnyEntityType(EntityTypes.Facility)]
        [HasPermission(Permissions.AcceptPriceOffer)]
        public async Task<IActionResult> AcceptOfferAsync(long offerId)
            => Result(await offerService.AcceptOfferAsync(offerId));

        [HttpPatch("reject")]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        [AnyEntityType(EntityTypes.Facility)]
        [HasPermission(Permissions.RejectPriceOffer)]
        public async Task<IActionResult> RejectOfferAsync(long offerId)
            => Result(await offerService.RejectOfferAsync(offerId));

        [HttpPatch("cancel")]
        [HasPermission(Permissions.CancelPriceOffer)]
        [AnyEntityType(EntityTypes.Company)]
        public async Task<IActionResult> CancelOfferAsync(long offerId)
            => Result(await offerService.CancelOfferAsync(offerId));

        [HttpPatch("hide")]
        [AnyEntityType(EntityTypes.Company, EntityTypes.Facility)]
        [HasPermission(Permissions.HidePriceOffer)]
        public async Task<IActionResult> HidePriceOfferAsync(long priceOfferId)
             => Result(await offerService.HideOfferAsync(priceOfferId));

        [HttpPatch("show")]
        [AnyEntityType(EntityTypes.Company, EntityTypes.Facility)]
        [HasPermission(Permissions.ShowPriceOffer)]

        public async Task<IActionResult> ShowPriceOfferAsync(long priceOfferId)
            => Result(await offerService.ShowOfferAsync(priceOfferId));



        [HttpGet("getMyCompanyOffers")]
        [HasPermission(Permissions.ViewPriceOffers)]
        [AnyEntityType(EntityTypes.Company)]
        public async Task<IActionResult> GetMyCompanyOffersAsync()
            => Result(await offerService.GetMyCompanyOffersAsync());


        [HttpGet("getMyCompanyOffersByRequestId")]
        [HasPermission(Permissions.ViewPriceOffers)]
        [AnyEntityType(EntityTypes.Company)]
        public async Task<IActionResult> GetMyCompanyOffersByRequestIdAsync(long requestId)
             => Result(await offerService.GetMyCompanyOffersByRequestIdAsync(requestId));

        [HttpGet("getMyFacilityOffers")]
        [HasPermission(Permissions.ViewPriceOffers)]
        [AnyEntityType(EntityTypes.Facility)]
        public async Task<IActionResult> GetMyFacilityOffersAsync()
            => Result(await offerService.GetMyFacilityOffersAsync());

        [HttpGet("getMyFacilityOffersByRequestId")]
        [HasPermission(Permissions.ViewPriceOffers)]
        [AnyEntityType(EntityTypes.Facility)]
        public async Task<IActionResult> GetMyFacilityOffersByRequestIdAsync(long requestId)
            => Result(await offerService.GetMyFacilityOffersByRequestIdAsync(requestId));
    }

}
