using Application.Guards.Dtos;
using Application.Guards.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/guards")]
    public class GuardsController: ApiControllerBase
    {
        private readonly IGuardsService _guardsService;

        public GuardsController(IGuardsService guardsService)
        {
            _guardsService = guardsService;
        }

        [HttpPost("create")]
        //[HasPermission(Permissions.CreateGuard)]
        public async Task<IActionResult> CreateAsync(CreateGuardDto dto)
            => Result(await _guardsService.CreateAsync(dto));

        [HttpPut("update")]
        //[HasPermission(Permissions.UpdateGuard)]
        public async Task<IActionResult> UpdateAsync(UpdateGuardDto dto)
            => Result(await _guardsService.UpdateAsync(dto));

        [HttpGet("getById")]
        //[HasPermission(Permissions.ViewGuards)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await _guardsService.GetByIdAsync(id));

        [HttpGet("getAll")]
        //[HasPermission(Permissions.ViewGuards)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _guardsService.GetAllAsync());

        [HttpDelete("softDelete")]
        //[HasPermission(Permissions.DeleteGuard)]
        public async Task<IActionResult> SoftDeleteAsync(long id)
            => Result(await _guardsService.SoftDeleteAsync(id));

        [HttpDelete("hardDelete")]
        //[HasPermission(Permissions.DeleteGuard)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await _guardsService.HardDeleteAsync(id));
    }
}
