using Application.Facilities.Dtos;
using Application.Facilities.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/facilities")]
    public class FacilitiesController : ApiControllerBase
    {
        private readonly IFacilitiesService _facilitiesService;

        public FacilitiesController(IFacilitiesService facilitiesService)
        {
            _facilitiesService = facilitiesService;
        }


        [HttpPost("create")]
        //[HasPermission(Permissions.CreateFacility)]
        public async Task<IActionResult> CreateAsync(CreateFacilityDto dto)
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
