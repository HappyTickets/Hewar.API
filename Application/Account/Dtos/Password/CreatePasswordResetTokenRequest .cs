using Localization.ResourceFiles;
using System.ComponentModel.DataAnnotations;

namespace Application.AccountManagement.Dtos.Password;

public class CreatePasswordResetTokenRequest
{
    [Required(ErrorMessageResourceName = nameof(Resource.Email_Required_Validation), ErrorMessageResourceType = typeof(Resource))]
    [RegularExpression(RegexTemplates.Email, ErrorMessageResourceName = nameof(Resource.Email_Format_Validation), ErrorMessageResourceType = typeof(Resource))]
    public string Email { get; set; } = string.Empty;
}
