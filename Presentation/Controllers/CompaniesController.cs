using Application.Companies.Dtos;
using Application.Companies.Service;
using Application.Guards.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/companies")]
    public class CompaniesController : ApiControllerBase
    {
        private readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateCompanyDto dto)
            => Result(await _companiesService.UpdateAsync(dto));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await _companiesService.GetByIdAsync(id));

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _companiesService.GetAllAsync()); 
        
        [HttpDelete("softDelete")]
        public async Task<IActionResult> SoftDeleteAsync(long id)
            => Result(await _companiesService.SoftDeleteAsync(id));
        
        [HttpDelete("hardDelete")]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await _companiesService.HardDeleteAsync(id));
    }
}
