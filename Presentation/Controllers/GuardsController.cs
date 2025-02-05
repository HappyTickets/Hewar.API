using Application.AccountManagement.Dtos.Authentication;
using Application.Guards.Dtos;
using Application.Guards.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class GuardsController(IGuardsService guardsService) : ApiControllerBase
    {
        [HttpPost("create")]
        //[HasPermission(Permissions.CreateGuard)]
        public async Task<IActionResult> CreateAsync(RegisterGuardRequest dto)
            => Result(await guardsService.CreateAsync(dto));

        [HttpPut("update")]
        //[HasPermission(Permissions.UpdateGuard)]
        public async Task<IActionResult> UpdateAsync(UpdateGuardDto dto)
            => Result(await guardsService.UpdateAsync(dto));

        [HttpGet("getById")]
        //[HasPermission(Permissions.ViewGuards)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await guardsService.GetByIdAsync(id));

        [HttpGet("getAll")]
        //[HasPermission(Permissions.ViewGuards)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await guardsService.GetAllAsync());

        [HttpDelete]
        //[HasPermission(Permissions.DeleteGuard)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await guardsService.DeleteAsync(id));
    }
}
