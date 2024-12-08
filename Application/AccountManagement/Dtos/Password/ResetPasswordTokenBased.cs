using System.ComponentModel.DataAnnotations;

namespace Application.AccountManagement.Dtos.Password;

public class ResetPasswordTokenDto
{
    [Required]
    public string Token { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string NewPassword { get; set; } = string.Empty;
    [Required, Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}

