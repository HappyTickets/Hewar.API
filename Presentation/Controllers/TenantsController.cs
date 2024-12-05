using Application.Tenants.Dtos;
using Application.Tenants.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("tenants")]
    public class TenantsController : ApiControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TenantBriefDto dto)
            => Result(await _tenantService.CreateAsync(dto));

        [HttpGet("{id}")]
        public async Task<IActionResult> Create(int id)
            => Result(await _tenantService.GetByIdAsync(id));
    }
}
