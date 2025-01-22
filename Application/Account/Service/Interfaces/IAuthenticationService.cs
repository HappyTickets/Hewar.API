using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Dtos.User;

namespace Application.AccountManagement.Service.Interfaces;

public interface IAuthenticationService
{


    Task<Result<AccountSessionDto>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    Task<Result<Empty>> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<Result<AccountSessionDto>> RefreshTokenAsync(RefreshAuthTokenRequest request, CancellationToken cancellationToken = default);
    Task<Result<Empty>> RegisterCompanyAsync(RegisterCompanyRequest registerRequest, CancellationToken cancellationToken = default);
    Task<Result<Empty>> RegisterFacilityAsync(RegisterFacilityRequest registerRequest, CancellationToken cancellationToken = default);
    Task<Result<Empty>> RegisterGuardAsync(RegisterGuardRequest registerRequest, CancellationToken cancellationToken = default);
}


