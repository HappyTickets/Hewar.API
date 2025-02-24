using Application.AccountManagement.Dtos.Authentication;
using Application.Companies.Dtos;
using Application.Companies.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Company
{
    [Authorize]
    public class CompaniesController(ICompaniesService companiesService) : ApiControllerBase
    {
        [HttpPost("create")]
        [HasPermission(Permissions.CreateCompany)]
        public async Task<IActionResult> CreateAsync(RegisterCompanyRequest dto)
            => Result(await companiesService.CreateAsync(dto));

        [HttpPut("update")]
        [HasPermission(Permissions.UpdateCompany)]
        public async Task<IActionResult> UpdateAsync(UpdateCompanyDto dto)
            => Result(await companiesService.UpdateAsync(dto));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await companiesService.GetByIdAsync(id));

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
            => Result(await companiesService.GetAllAsync());

        [HttpDelete("softDelete")]
        [HasPermission(Permissions.DeleteCompany)]
        public async Task<IActionResult> SoftDeleteAsync(long id)
            => Result(await companiesService.SoftDeleteAsync(id));

        [HttpDelete("hardDelete")]
        [HasPermission(Permissions.DeleteCompany)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await companiesService.HardDeleteAsync(id));
    }
}
