using Application.AccountManagement.Dtos.Token;

namespace Application.AccountManagement.Service.Interfaces;

public interface ITokensService
{
    Task<TokensInfo> GenerateTokensAsync(ApplicationUser user);
    Task<TokensInfo?> RefreshAsync(string accessToken);
    Task RemoveRefreshTokenAsync();
    Task RemoveExpiredTokensAsync();

}
