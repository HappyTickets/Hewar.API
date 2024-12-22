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

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateGuardDto dto)
            => Result(await _guardsService.UpdateAsync(dto));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await _guardsService.GetByIdAsync(id));

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _guardsService.GetAllAsync());

        [HttpDelete("softDelete")]
        public async Task<IActionResult> SoftDeleteAsync(long id)
            => Result(await _guardsService.SoftDeleteAsync(id));

        [HttpDelete("hardDelete")]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await _guardsService.HardDeleteAsync(id));
    }
}
