using Application.AccountManagement.Dtos.Password;
using FluentValidation;


namespace Application.AccountManagement.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {

        public ChangePasswordRequestValidator()
        {

            RuleFor(x => x.OldPassword)
             .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
             .Matches(RegexTemplates.Password).WithState(_ => (int)ValidationMsgs.Password_Format_Validation);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .Matches(RegexTemplates.Password).WithState(_ => (int)ValidationMsgs.Password_Format_Validation)
                .NotEqual(x => x.OldPassword).WithState(_ => (int)ValidationMsgs.RepeatedPassword);

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .Equal(x => x.NewPassword).WithState(_ => (int)ValidationMsgs.Passwords_NotMatching);
        }

    }
}
