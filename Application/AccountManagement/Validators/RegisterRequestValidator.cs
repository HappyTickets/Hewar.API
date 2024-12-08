using Application.AccountManagement.Dtos.Authentication;
using FluentValidation;
using Localization.ResourceFiles;

namespace Application.Authorization.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {

        public RegisterRequestValidator()
        {

            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(Resource.UserName_Validation)
            .Matches(@"^\S+$").WithMessage(Resource.UserName_NoSpaces)
            .MinimumLength(3).WithMessage(Resource.UserName_Length) // Minimum length of 3
            .MaximumLength(20).WithMessage(Resource.UserName_Length); // Maximum length of 20


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Resource.Password_Validation)
                .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(Resource.Confirming_Password_Validation)
                .Equal(x => x.Password).WithMessage(Resource.Passwords_NotMatching);
        }

    }
}
