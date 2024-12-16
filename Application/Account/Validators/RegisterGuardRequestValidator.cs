using Application.AccountManagement.Dtos.Authentication;
using FluentValidation;
using Localization.ResourceFiles;

namespace Application.Authorization.Validators
{
    public class RegisterGuardRequestValidator : AbstractValidator<RegisterGuardRequest>
    {
        public RegisterGuardRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .Must(un => !un.Contains(" ")).WithMessage(Resource.UserName_NoSpaces);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Resource.Password_Validation)
                .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Skills)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }

    }
}
