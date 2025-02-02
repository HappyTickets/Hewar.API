using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.AccountManagement;

public class AuthenticationController(IAuthenticationService authenticationService) : ApiControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [HttpPost("registerGuard")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterGuard([FromBody] RegisterGuardRequest registerRequest, CancellationToken cancellationToken = default)
    {
        return Result(await _authenticationService.RegisterGuardAsync(registerRequest, cancellationToken));
    }

    [HttpPost("registerFacility")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterFacility([FromBody] RegisterFacilityRequest registerRequest, CancellationToken cancellationToken = default)
    {
        return Result(await _authenticationService.RegisterFacilityAsync(registerRequest, cancellationToken));
    }

    [HttpPost("registerCompany")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyRequest registerRequest, CancellationToken cancellationToken = default)
    {
        return Result(await _authenticationService.RegisterCompanyAsync(registerRequest, cancellationToken));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken = default)

    {
        var res = await _authenticationService.LoginAsync(loginRequest, cancellationToken);
        return Result(res);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken = default)
    {
        return Result(await _authenticationService.LogoutAsync(cancellationToken));
    }

    [HttpPost("refreshToken")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshAuthToken([FromBody] RefreshAuthTokenRequest tokens, CancellationToken cancellationToken = default)
    {
        var res = await _authenticationService.RefreshTokenAsync(tokens, cancellationToken);
        return Result(res);
    }
}

