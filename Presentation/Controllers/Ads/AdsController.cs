using Application.Ads.Dtos.Post;
using Application.Ads.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Ads
{
    public class AdsController(IAdsService insuranceAdsService) : ApiControllerBase
    {
        [HttpPost("create")]
        [AnyEntityType(EntityTypes.Facility)]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        public async Task<IActionResult> CreateAdAsync(CreateAdDto dto)
            => Result(await insuranceAdsService.CreateAdAsync(dto));

        [HttpPut("update")]
        [AnyEntityType(EntityTypes.Facility)]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        public async Task<IActionResult> UpdateAdAsync(UpdateAdDto dto)
            => Result(await insuranceAdsService.UpdateAdAsync(dto));

        [HttpPatch("changeStatus")]
        [AnyEntityType(EntityTypes.Facility)]
        public async Task<IActionResult> ChangeAdStatusAsync(long id, AdStatus status)
            => Result(await insuranceAdsService.ChangeAdStatusAsync(id, status));

        [HttpGet("getAdById")]
        public async Task<IActionResult> GetAdByIdAsync(long id)
            => Result(await insuranceAdsService.GetAdByIdAsync(id));

        [HttpGet("getMyAds")]
        [AnyEntityType(EntityTypes.Facility)]
        public async Task<IActionResult> GetMyAdsAsync()
            => Result(await insuranceAdsService.GetMyAdsAsync());

        [HttpGet("getOpened")]
        public async Task<IActionResult> GetOpenedAdsAsync()
            => Result(await insuranceAdsService.GetOpenedAdsAsync());
        [HttpDelete("delete")]
        [AnyEntityType(EntityTypes.Facility)]
        public async Task<IActionResult> DeleteAdAsync(long id)
            => Result(await insuranceAdsService.DeleteAdAsync(id));
    }
}
