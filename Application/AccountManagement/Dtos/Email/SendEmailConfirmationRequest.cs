using System.ComponentModel.DataAnnotations;

namespace Application.AccountManagement.Dtos.Email;

public class SendEmailConfirmationRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}
