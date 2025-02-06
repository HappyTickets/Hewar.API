using Application.Hewar.Dtos;
using Application.Hewar.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class HewarServicesController : ApiControllerBase
    {
        private readonly IHewarProvidedService _hewarService;

        public HewarServicesController(IHewarProvidedService hewarService)
        {
            _hewarService = hewarService;
        }

        [HttpPost("create")]
        //[HasPermission(Permissions.CreateHewar)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateHewarServiceDto dto)

            => Result(await _hewarService.CreateAsync(dto));



        [HttpPut("update")]
        //[HasPermission(Permissions.UpdateHewar)]
        public async Task<IActionResult> UpdateAsync([FromBody] HewarServiceDto dto)
       => Result(await _hewarService.UpdateAsync(dto));

        [HttpGet("getById")]
        //[HasPermission(Permissions.View)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await _hewarService.GetByIdAsync(id));


        [HttpGet("getAll")]
        //[HasPermission(Permissions.View)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _hewarService.GetAllAsync());


        [HttpDelete("Delete")]
        //[HasPermission(Permissions.DeleteHewar)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await _hewarService.DeleteAsync(id));
    }

}
