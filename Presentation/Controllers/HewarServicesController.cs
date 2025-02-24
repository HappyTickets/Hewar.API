using Application.Hewar.Dtos;
using Application.Hewar.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class HewarServicesController(IHewarProvidedService hewarService) : ApiControllerBase
    {
        [HttpPost("create")]
        [HasPermission(Permissions.CreateHewarService)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateHewarServiceDto dto)

            => Result(await hewarService.CreateAsync(dto));



        [HttpPut("update")]
        [HasPermission(Permissions.UpdateHewarService)]
        public async Task<IActionResult> UpdateAsync([FromBody] HewarServiceDto dto)
       => Result(await hewarService.UpdateAsync(dto));

        [HttpGet("getById")]
        [HasPermission(Permissions.ViewHewarServices)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await hewarService.GetByIdAsync(id));


        [HttpGet("getAll")]
        [HasPermission(Permissions.ViewHewarServices)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await hewarService.GetAllAsync());


        [HttpDelete("Delete")]
        [HasPermission(Permissions.DeleteHewarService)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await hewarService.DeleteAsync(id));
    }

}
