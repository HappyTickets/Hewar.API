using Application.InsuranceAds.Dtos;
using Application.InsuranceAds.Service;
using Microsoft.AspNetCore.Authorization;
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
    }
}
