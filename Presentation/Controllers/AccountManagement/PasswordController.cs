using Application.AccountManagement.Dtos.Password;
using Application.AccountManagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.AccountManagement;

public class PasswordController : ApiControllerBase
{
    private readonly IPasswordResetService _passwordResetService;

    public PasswordController(IPasswordResetService passwordResetService)
    {
        _passwordResetService = passwordResetService;
    }

    [HttpPost("create-reset-token")]
    [AllowAnonymous]
    public async Task<IActionResult> CreatePasswordResetToken([FromBody] CreatePasswordResetTokenRequest request, CancellationToken cancellationToken = default)
    {
        return Result(await _passwordResetService.CreatePasswordResetTokenAsync(request, cancellationToken));
    }

    [HttpPost("reset")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordTokenDto dto, CancellationToken cancellationToken = default)
    {
        return Result(await _passwordResetService.ResetPasswordAsync(dto, cancellationToken));
    }

    [HttpPatch("change")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        return Result(await _passwordResetService.ResetPasswordAsync(request, cancellationToken));
    }
}

