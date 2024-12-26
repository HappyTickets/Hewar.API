using Application.InsuranceAds.Dtos;
using Application.InsuranceAds.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/insuranceAds")]
    public class InsuranceAdsController : ApiControllerBase
    {
        private readonly IInsuranceAdsService _insuranceAdsService;

        public InsuranceAdsController(IInsuranceAdsService insuranceAdsService)
        {
            _insuranceAdsService = insuranceAdsService;
        }

        [HttpPost("createAd")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CreateAdAsync(CreateInsuranceAdDto dto)
            => Result(await _insuranceAdsService.CreateAdAsync(dto));
        
        [HttpPut("updateAd")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> UpdateAdAsync(UpdateInsuranceAdDto dto)
            => Result(await _insuranceAdsService.UpdateAdAsync(dto));
        
        [HttpGet("getAdById")]
        public async Task<IActionResult> GetAdByIdAsync(long id)
            => Result(await _insuranceAdsService.GetAdByIdAsync(id));
        
        [HttpGet("getMyAds")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyAdsAsync()
            => Result(await _insuranceAdsService.GetMyAdsAsync());
        
        [HttpGet("getOpenedAds")]
        public async Task<IActionResult> GetOpenedAdsAsync()
            => Result(await _insuranceAdsService.GetOpenedAdsAsync());
        
        [HttpPost("createOffer")]
        [HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> CreateOfferAsync(CreateInsuranceAdOfferDto dto)
            => Result(await _insuranceAdsService.CreateOfferAsync(dto));
        
        [HttpPatch("acceptOffer")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> AcceptOfferAsync(long offerId)
            => Result(await _insuranceAdsService.AcceptOfferAsync(offerId));
        
        [HttpPatch("rejectOffer")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> RejectOfferAsync(long offerId)
            => Result(await _insuranceAdsService.RejectOfferAsync(offerId));

        [HttpGet("getMyOffersByAdIdAsFacility")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyOffersByAdIdAsFacilityAsync(long adId)
            => Result(await _insuranceAdsService.GetMyOffersByAdIdAsFacilityAsync(adId));

        [HttpGet("getMyOffersAsFacility")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyOffersAsFacilityAsync()
            => Result(await _insuranceAdsService.GetMyOffersAsFacilityAsync());

        [HttpGet("getMyOffersByAdIdAsCompany")]
        [HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyOffersByAdIdAsCompanyAsync(long adId)
            => Result(await _insuranceAdsService.GetMyOffersByAdIdAsCompanyAsync(adId));
        
        [HttpGet("getMyOffersAsCompany")]
        [HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyOffersAsCompanyAsync()
            => Result(await _insuranceAdsService.GetMyOffersAsCompanyAsync());

        [HttpPost("CreateMessage")]
        [HaveAccountTypes(AccountTypes.Company, AccountTypes.Facility)]
        public async Task<IActionResult> CreateMessageAsync(CreateInsuranceAdOfferMessageDto dto)
            => Result(await _insuranceAdsService.CreateMessageAsync(dto));
        
        [HttpGet("getMessages")]
        [HaveAccountTypes(AccountTypes.Company, AccountTypes.Facility)]
        public async Task<IActionResult> GetMessagesAsync(long offerId)
            => Result(await _insuranceAdsService.GetMessagesAsync(offerId));
    }
}
