﻿using Application.AccountManagement.Dtos.Authentication;
using Application.Companies.Dtos;
using Application.Companies.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Company
{
    public class CompaniesController : ApiControllerBase
    {
        private readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        [HttpPost("create")]
        //[HasPermission(Permissions.CreateCompanyAsync)]
        public async Task<IActionResult> CreateAsync(RegisterCompanyRequest dto)
            => Result(await _companiesService.CreateAsync(dto));

        [HttpPut("update")]
        //[HasPermission(Permissions.UpdateCompany)]
        public async Task<IActionResult> UpdateAsync(UpdateCompanyDto dto)
            => Result(await _companiesService.UpdateAsync(dto));

        [HttpGet("getById")]
        //[HasPermission(Permissions.ViewCompanies)]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Result(await _companiesService.GetByIdAsync(id));

        [HttpGet("getAll")]
        //[HasPermission(Permissions.ViewCompanies)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _companiesService.GetAllAsync());

        [HttpDelete("softDelete")]
        //[HasPermission(Permissions.DeleteCompany)]
        public async Task<IActionResult> SoftDeleteAsync(long id)
            => Result(await _companiesService.SoftDeleteAsync(id));

        [HttpDelete("hardDelete")]
        //[HasPermission(Permissions.DeleteCompany)]
        public async Task<IActionResult> HardDeleteAsync(long id)
            => Result(await _companiesService.HardDeleteAsync(id));
    }
}
