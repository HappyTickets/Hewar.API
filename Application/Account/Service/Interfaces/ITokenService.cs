using Application.AccountManagement.Dtos.Token;

namespace Application.AccountManagement.Service.Interfaces;

public interface ITokensService
{
    public Task SaveRefreshTokenAsync(long userId, RefreshTokenDto refreshToken);
    public Task<TokensInfo> GenerateTokensAsync(ApplicationUser user);
    public Task<TokensInfo?> RefreshAsync(string accessToken, string refreshToken);
    public Task RemoveRefreshTokenAsync(string refreshToken);
    public Task RemoveExpiredTokensAsync();

}
