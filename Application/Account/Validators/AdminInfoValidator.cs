using Application.Account.Dtos.User;
using FluentValidation;

namespace Application.Account.Validators
{
    internal class AdminInfoValidator : AbstractValidator<AdminInfoDto>
    {
        internal AdminInfoValidator()
        {
            RuleFor(f => f.Email)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
            .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Email_Format_Validation);

            RuleFor(f => f.Phone)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.Password)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Password_Validation)
                .Matches(RegexTemplates.Password).WithState(_ => (int)ValidationMsgs.Password_Format_Validation);

            RuleFor(f => f.FirstName)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.LastName)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

        }
    }
}
