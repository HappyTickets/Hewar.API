using System.ComponentModel.DataAnnotations;

namespace Application.AccountManagement.Dtos.Password;

public class CreatePasswordResetTokenRequest
{
    [Required]
    [RegularExpression(RegexTemplates.Email)]
    public string Email { get; set; } = string.Empty;
}
