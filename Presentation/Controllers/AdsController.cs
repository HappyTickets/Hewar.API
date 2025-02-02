using Application.Ads.Dtos.Offers;
using Application.Ads.Dtos.Post;
using Application.Ads.Service;
using Application.Chats.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AdsController : ApiControllerBase
    {
        private readonly IAdsService _adsService;

        public AdsController(IAdsService insuranceAdsService)
        {
            _adsService = insuranceAdsService;
        }

        [HttpPost("createAd")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CreateAdAsync(CreateAdDto dto)
            => Result(await _adsService.CreateAdAsync(dto));

        [HttpPut("updateAd")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> UpdateAdAsync(UpdateAdDto dto)
            => Result(await _adsService.UpdateAdAsync(dto));

        [HttpGet("getAdById")]
        public async Task<IActionResult> GetAdByIdAsync(long id)
            => Result(await _adsService.GetAdByIdAsync(id));

        [HttpGet("getMyAds")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyAdsAsync()
            => Result(await _adsService.GetMyAdsAsync());

        [HttpGet("getOpenedAds")]
        public async Task<IActionResult> GetOpenedAdsAsync()
            => Result(await _adsService.GetOpenedAdsAsync());

        [HttpPost("createOffer")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> CreateOfferAsync(CreateAdOfferDto dto)
            => Result(await _adsService.CreateOfferAsync(dto));

        [HttpPatch("acceptOffer")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> AcceptOfferAsync(long offerId)
            => Result(await _adsService.AcceptOfferAsync(offerId));

        [HttpPatch("rejectOffer")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> RejectOfferAsync(long offerId)
            => Result(await _adsService.RejectOfferAsync(offerId));

        [HttpPatch("cancelOffer")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> CancelOfferAsync(long offerId)
            => Result(await _adsService.CancelOfferAsync(offerId));

        [HttpGet("getMyOffersByAdIdAsFacility")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyOffersByAdIdAsFacilityAsync(long adId)
            => Result(await _adsService.GetMyOffersByAdIdAsFacilityAsync(adId));

        [HttpGet("getMyOffersAsFacility")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyOffersAsFacilityAsync()
            => Result(await _adsService.GetMyOffersAsFacilityAsync());

        [HttpGet("getMyOffersByAdIdAsCompany")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyOffersByAdIdAsCompanyAsync(long adId)
            => Result(await _adsService.GetMyOffersByAdIdAsCompanyAsync(adId));

        [HttpGet("getMyOffersAsCompany")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyOffersAsCompanyAsync()
            => Result(await _adsService.GetMyOffersAsCompanyAsync());


        [HttpGet("initializeAdOfferChat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzeAdOfferChatAsync(long adOfferId)
           => Result(await _adsService.InitialzeAdOfferChatAsync(adOfferId));

        [HttpPost("createAdOfferMessage")]
        //[HaveAccountTypes(AccountTypes.Company, AccountTypes.Facility)]
        public async Task<IActionResult> CreateAdOfferMessageAsync(CreateChatMessageDto dto)
            => Result(await _adsService.CreateAdOfferMessageAsync(dto));

        [HttpGet("getAdOfferChatMessages")]
        //[HaveAccountTypes(AccountTypes.Company, AccountTypes.Facility)]
        public async Task<IActionResult> GetAdOfferChatMessagesAsync(long chatId)
            => Result(await _adsService.GetAdOfferChatMessagesAsync(chatId));
    }
}
