using Application.Companies.Dtos.ProvidedServices;
using Application.Companies.Service.ServicesProvided;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Company
{
    [Authorize]
    public class CompanyServicesController(ICompanyProvidedService companyService) : ApiControllerBase
    {
        [HttpPost("create")]
        [HasPermission(Permissions.CreateCompanyService)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyServiceDto dto)

            => Result(await companyService.CreateAsync(dto));

        [HttpPut("update")]
        [HasPermission(Permissions.UpdateCompanyService)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCompanyServiceDto dto)
       => Result(await companyService.UpdateAsync(dto));

        [HttpGet("getById")]
        [HasPermission(Permissions.ViewCompaniesService)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await companyService.GetByIdAsync(id));

        [HttpGet("getServicesByCompanyId")]
        [HasPermission(Permissions.ViewCompaniesService)]
        public async Task<IActionResult> GetServicesByCompanyIdAsync(long companyId)
            => Result(await companyService.GetServicesByCompanyIdAsync(companyId));


        [HttpGet("getAll")]
        [HasPermission(Permissions.ViewCompaniesService)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await companyService.GetAllAsync());


        [HttpDelete("Delete")]
        [HasPermission(Permissions.DeleteCompany)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await companyService.DeleteAsync(id));
    }

}
