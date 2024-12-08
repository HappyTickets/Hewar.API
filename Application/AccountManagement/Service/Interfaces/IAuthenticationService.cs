using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Dtos.User;

namespace Application.AccountManagement.Service.Interfaces;

public interface IAuthenticationService
{

    Task<Result<Empty>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    Task<Result<UserSessionDto>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    Task<Result<Empty>> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<Result<UserSessionDto>> RefreshTokenAsync(RefreshAuthTokenRequest request, CancellationToken cancellationToken = default);
}


