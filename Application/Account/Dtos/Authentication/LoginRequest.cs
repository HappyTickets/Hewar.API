using Localization.ResourceFiles;
using System.ComponentModel.DataAnnotations;

namespace Application.AccountManagement.Dtos.Authentication;

public class LoginRequest
{
    [Required(ErrorMessageResourceName = nameof(Resource.Email_Required_Validation), ErrorMessageResourceType = typeof(Resource))]
    [RegularExpression(RegexTemplates.Email, ErrorMessageResourceName = nameof(Resource.Email_Format_Validation), ErrorMessageResourceType = typeof(Resource))]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessageResourceName = nameof(Resource.Password_Validation), ErrorMessageResourceType = typeof(Resource))]
    public string Password { get; set; } = string.Empty;
}
