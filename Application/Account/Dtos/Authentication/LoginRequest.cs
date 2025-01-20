using System.ComponentModel.DataAnnotations;

namespace Application.AccountManagement.Dtos.Authentication;

public class LoginRequest
{
    [Required]
    [RegularExpression(RegexTemplates.Email)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
