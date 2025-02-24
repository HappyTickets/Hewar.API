using Application.SecurityCertificates.DTOs;
using Application.SecurityCertificates.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public class SecurityCertificateController(ISecurityCertificateService contractService) : ApiControllerBase
{
    [HttpPost("create")]
    [HasPermission(Permissions.CreateSecurityCertificate)]
    public async Task<IActionResult> Create(SecurityCertificateCreateDto createDto)
        => Result(await contractService.CreateAsync(createDto));

    [HttpPut("update")]
    [HasPermission(Permissions.UpdateSecurityCertificate)]
    public async Task<IActionResult> Update(SecurityCertificateUpdateDto updateDto)
        => Result(await contractService.UpdateAsync(updateDto));

    [HttpDelete("delete")]
    [HasPermission(Permissions.DeleteSecurityCertificate)]
    public async Task<IActionResult> Delete(long id)
        => Result(await contractService.DeleteAsync(id));

    [HttpGet("getById")]
    [HasPermission(Permissions.ViewSecurityCertificate)]
    public async Task<IActionResult> GetById(long id)
        => Result(await contractService.GetByIdAsync(id));

    [HttpGet("getByFacilityId")]
    [HasPermission(Permissions.ViewSecurityCertificate)]
    public async Task<IActionResult> GetByFacilityId(long facilityId)
        => Result(await contractService.GetByFacilityIdAsync(facilityId));

    [HttpGet("getAll")]
    [HasPermission(Permissions.ViewSecurityCertificate)]
    public async Task<IActionResult> GetAll() => Result(await contractService.GetAllAsync());


    [HttpPatch("approve")]
    [HasPermission(Permissions.ApproveSecurityCertificate)]
    public async Task<IActionResult> Approve(long id)
        => Result(await contractService.ApproveAsync(id));

    [HttpPatch("reject")]
    [HasPermission(Permissions.RejectSecurityCertificate)]
    public async Task<IActionResult> Reject(long id)
        => Result(await contractService.RejectAsync(id));
}


