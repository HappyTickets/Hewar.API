using Application.Companies.Dtos;
using Application.Companies.Service;
using Application.Facilities.Dtos;
using Application.Facilities.Service;
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


        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateFacilityDto dto)
            => Result(await _facilitiesService.UpdateAsync(dto));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await _facilitiesService.GetByIdAsync(id));

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _facilitiesService.GetAllAsync());

        [HttpDelete("softDelete")]
        public async Task<IActionResult> SoftDeleteAsync(long id)
            => Result(await _facilitiesService.SoftDeleteAsync(id));

        [HttpDelete("hardDelete")]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await _facilitiesService.HardDeleteAsync(id));
    }
}
