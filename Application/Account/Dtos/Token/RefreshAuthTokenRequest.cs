using System.ComponentModel.DataAnnotations;

namespace Application.AccountManagement.Dtos.Token;

public class RefreshAuthTokenRequest
{
    [Required]
    public string AccessToken { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}
