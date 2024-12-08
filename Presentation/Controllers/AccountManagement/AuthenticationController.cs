using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.AccountManagement;

public class AuthenticationController(IAuthenticationService authenticationService) : ApiControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken = default)
    {
        return Result(await _authenticationService.RegisterAsync(registerRequest, cancellationToken));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        var res = await _authenticationService.LoginAsync(loginRequest, cancellationToken);
        return Result(res);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken, CancellationToken cancellationToken = default)
    {
        return Result(await _authenticationService.LogoutAsync(refreshToken, cancellationToken));
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshAuthToken([FromBody] RefreshAuthTokenRequest tokens, CancellationToken cancellationToken = default)
    {
        var res = await _authenticationService.RefreshTokenAsync(tokens, cancellationToken);
        return Result(res);
    }
}

