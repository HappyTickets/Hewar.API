using Application.AccountManagement.Dtos.Password;
using FluentValidation;
using Localization.ResourceFiles;


namespace Application.AccountManagement.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {

        public ChangePasswordRequestValidator()
        {

            RuleFor(x => x.OldPassword)
             .NotEmpty().WithMessage(Resource.RequiredField)
             .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation)
                .NotEqual(x => x.OldPassword).WithMessage(Resource.RepeatedPassword);

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .Equal(x => x.NewPassword).WithMessage(Resource.Passwords_NotMatching);
        }

    }
}
