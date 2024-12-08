namespace Application.AccountManagement.Dtos.Token;

public class TokenDto
{
    public string Token { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }

}

