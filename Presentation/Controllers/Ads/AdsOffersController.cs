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
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> CreateOfferAsync(CreateAdOfferDto dto)
          => Result(await adsService.CreateOfferAsync(dto));

        [HttpPatch("accept")]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> AcceptOfferAsync(long offerId)
            => Result(await adsService.AcceptOfferAsync(offerId));

        [HttpPatch("reject")]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> RejectOfferAsync(long offerId)
            => Result(await adsService.RejectOfferAsync(offerId));

        [HttpPatch("cancel")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> CancelOfferAsync(long offerId)
            => Result(await adsService.CancelOfferAsync(offerId));

        [HttpGet("getMyOffersByAdIdAsFacility")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyOffersByAdIdAsFacilityAsync(long adId)
            => Result(await adsService.GetMyOffersByAdIdAsFacilityAsync(adId));

        [HttpGet("getMyOffersAsFacility")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyOffersAsFacilityAsync()
            => Result(await adsService.GetMyOffersAsFacilityAsync());

        [HttpGet("getMyOffersByAdIdAsCompany")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyOffersByAdIdAsCompanyAsync(long adId)
            => Result(await adsService.GetMyOffersByAdIdAsCompanyAsync(adId));

        [HttpGet("getMyOffersAsCompany")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyOffersAsCompanyAsync()
            => Result(await adsService.GetMyOffersAsCompanyAsync());
        #endregion
    }
}
