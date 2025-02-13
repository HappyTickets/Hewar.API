using Application.SecurityCertificates.DTOs;
using Application.SecurityCertificates.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public class SecurityCertificateController(ISecurityCertificateService contractService) : ApiControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(SecurityCertificateCreateDto createDto)
        => Result(await contractService.CreateAsync(createDto));

    [HttpPut("update")]
    public async Task<IActionResult> Update(SecurityCertificateUpdateDto updateDto)
        => Result(await contractService.UpdateAsync(updateDto));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(long id)
        => Result(await contractService.DeleteAsync(id));

    [HttpGet("getById")]
    public async Task<IActionResult> GetById(long id)
        => Result(await contractService.GetByIdAsync(id));

    [HttpGet("getByFacilityId")]
    public async Task<IActionResult> GetByFacilityId(long facilityId)
        => Result(await contractService.GetByFacilityIdAsync(facilityId));

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll() => Result(await contractService.GetAllAsync());


    [HttpPatch("approve")]
    public async Task<IActionResult> Approve(long id)
        => Result(await contractService.ApproveAsync(id));

    [HttpPatch("reject")]
    public async Task<IActionResult> Reject(long id)
        => Result(await contractService.RejectAsync(id));
}


