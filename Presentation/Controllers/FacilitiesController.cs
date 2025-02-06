using Application.AccountManagement.Dtos.Authentication;
using Application.Facilities.Dtos;
using Application.Facilities.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class FacilitiesController(IFacilitiesService facilitiesService) : ApiControllerBase
    {
        [HttpPost("create")]
        //[HasPermission(Permissions.CreateFacilityAsync)]
        public async Task<IActionResult> CreateAsync(RegisterFacilityRequest dto)
            => Result(await facilitiesService.CreateAsync(dto));

        [HttpPut("update")]
        //[HasPermission(Permissions.UpdateFacility)]
        public async Task<IActionResult> UpdateAsync(UpdateFacilityDto dto)
            => Result(await facilitiesService.UpdateAsync(dto));

        [HttpGet("getById")]
        //[HasPermission(Permissions.ViewFacilities)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await facilitiesService.GetByIdAsync(id));

        [HttpGet("getAll")]
        //[HasPermission(Permissions.ViewFacilities)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await facilitiesService.GetAllAsync());

        [HttpDelete("softDelete")]
        //[HasPermission(Permissions.DeleteFacility)]
        public async Task<IActionResult> SoftDeleteAsync(long id)
            => Result(await facilitiesService.SoftDeleteAsync(id));

        [HttpDelete("hardDelete")]
        //[HasPermission(Permissions.DeleteFacility)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await facilitiesService.HardDeleteAsync(id));
    }
}
