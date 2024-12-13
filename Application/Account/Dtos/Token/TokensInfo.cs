namespace Application.AccountManagement.Dtos.Token;

public class TokensInfo
{
    public long UserId { get; set; }
    public TokenDto JWT { get; set; }
    public RefreshTokenDto Refresh { get; set; }
}
