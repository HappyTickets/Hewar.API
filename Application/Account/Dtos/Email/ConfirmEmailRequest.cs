using System.ComponentModel.DataAnnotations;

namespace Application.AccountManagement.Dtos.Email;

public class ConfirmEmailRequest
{
    [Required]
    public string VerificationCode { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}