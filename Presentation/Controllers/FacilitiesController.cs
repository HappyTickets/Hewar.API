using Application.AccountManagement.Dtos.Authentication;
using Application.Facilities.Dtos;
using Application.Facilities.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class FacilitiesController : ApiControllerBase
    {
        private readonly IFacilitiesService _facilitiesService;

        public FacilitiesController(IFacilitiesService facilitiesService)
        {
            _facilitiesService = facilitiesService;
        }


        [HttpPost("create")]
        //[HasPermission(Permissions.CreateFacilityAsync)]
        public async Task<IActionResult> CreateAsync(RegisterFacilityRequest dto)
            => Result(await _facilitiesService.CreateAsync(dto));

        [HttpPut("update")]
        //[HasPermission(Permissions.UpdateFacility)]
        public async Task<IActionResult> UpdateAsync(UpdateFacilityDto dto)
            => Result(await _facilitiesService.UpdateAsync(dto));

        [HttpGet("getById")]
        //[HasPermission(Permissions.ViewFacilities)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await _facilitiesService.GetByIdAsync(id));

        [HttpGet("getAll")]
        //[HasPermission(Permissions.ViewFacilities)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _facilitiesService.GetAllAsync());

        [HttpDelete("softDelete")]
        //[HasPermission(Permissions.DeleteFacility)]
        public async Task<IActionResult> SoftDeleteAsync(long id)
            => Result(await _facilitiesService.SoftDeleteAsync(id));

        [HttpDelete("hardDelete")]
        //[HasPermission(Permissions.DeleteFacility)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await _facilitiesService.HardDeleteAsync(id));
    }
}
