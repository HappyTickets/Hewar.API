using Application.Ads.Dtos.Offers;
using Application.Ads.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Ads
{
    public class AdsOffersController(IAdsService adsService) : ApiControllerBase
    {
        #region Ads Offer
        [HttpPost("create")]
        [AnyEntityType(EntityTypes.Company)]
        public async Task<IActionResult> CreateOfferAsync(CreateAdOfferDto dto)
          => Result(await adsService.CreateOfferAsync(dto));


        [HttpPut("update")]
        [AnyEntityType(EntityTypes.Company)]
        public async Task<IActionResult> UpdateAdOfferAsync(UpdateAdOfferDto dto)
    => Result(await adsService.UpdateAdOfferAsync(dto));

        [HttpPatch("hide")]
        [AnyEntityType(EntityTypes.Facility, EntityTypes.Company)]
        public async Task<IActionResult> HideOfferAsync(long adOfferId)
            => Result(await adsService.HideOfferAsync(adOfferId));

        [HttpPatch("show")]
        [AnyEntityType(EntityTypes.Facility, EntityTypes.Company)]
        public async Task<IActionResult> ShowOfferAsync(long adOfferId)
            => Result(await adsService.ShowOfferAsync(adOfferId));

        [HttpPatch("accept")]
        [AnyEntityType(EntityTypes.Facility)]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        public async Task<IActionResult> AcceptOfferAsync(long offerId)
            => Result(await adsService.AcceptOfferAsync(offerId));

        [HttpPatch("reject")]
        [AnyEntityType(EntityTypes.Facility)]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        public async Task<IActionResult> RejectOfferAsync(long offerId)
            => Result(await adsService.RejectOfferAsync(offerId));

        [HttpPatch("cancel")]
        [AnyEntityType(EntityTypes.Company)]
        public async Task<IActionResult> CancelOfferAsync(long offerId)
            => Result(await adsService.CancelOfferAsync(offerId));

        [HttpGet("getMyOffersByAdIdAsFacility")]
        [AnyEntityType(EntityTypes.Facility)]
        public async Task<IActionResult> GetMyOffersByAdIdAsFacilityAsync(long adId)
            => Result(await adsService.GetMyOffersByAdIdAsFacilityAsync(adId));

        [HttpGet("getMyOffersAsFacility")]
        [AnyEntityType(EntityTypes.Facility)]
        public async Task<IActionResult> GetMyOffersAsFacilityAsync()
            => Result(await adsService.GetMyOffersAsFacilityAsync());

        [HttpGet("getMyOffersByAdIdAsCompany")]
        [AnyEntityType(EntityTypes.Company)]
        public async Task<IActionResult> GetMyOffersByAdIdAsCompanyAsync(long adId)
            => Result(await adsService.GetMyOffersByAdIdAsCompanyAsync(adId));

        [HttpGet("getMyOffersAsCompany")]
        [AnyEntityType(EntityTypes.Company)]
        public async Task<IActionResult> GetMyOffersAsCompanyAsync()
            => Result(await adsService.GetMyOffersAsCompanyAsync());
        #endregion
    }
}
