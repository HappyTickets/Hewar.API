namespace Application.AccountManagement.Dtos.Token;

public class RefreshTokenDto
{
    public string Token { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
}
