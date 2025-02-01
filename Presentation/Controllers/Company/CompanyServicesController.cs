using Application.Companies.Dtos.ProvidedServices;
using Application.Companies.Service.ServicesProvided;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Company
{
    [Route("api/[controller]")]

    public class CompanyServicesController : ApiControllerBase
    {
        private readonly ICompanyProvidedService _companyService;

        public CompanyServicesController(ICompanyProvidedService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("create")]
        //[HasPermission(Permissions.CreateCompanyAsync)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyServiceDto dto)

            => Result(await _companyService.CreateAsync(dto));



        [HttpPut("update")]
        //[HasPermission(Permissions.UpdateCompany)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCompanyServiceDto dto)
       => Result(await _companyService.UpdateAsync(dto));

        [HttpGet("getById/{id}")]
        //[HasPermission(Permissions.ViewCompanies)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await _companyService.GetByIdAsync(id));


        [HttpGet("getAll")]
        //[HasPermission(Permissions.ViewCompanies)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _companyService.GetAllAsync());


        [HttpDelete("{id}")]
        //[HasPermission(Permissions.DeleteCompany)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await _companyService.DeleteAsync(id));
    }

}
