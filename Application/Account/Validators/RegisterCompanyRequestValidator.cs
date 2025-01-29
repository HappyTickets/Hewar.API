using Application.AccountManagement.Dtos.Authentication;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class RegisterCompanyRequestValidator : AbstractValidator<RegisterCompanyRequest>
    {

        public RegisterCompanyRequestValidator()
        {

            RuleFor(c => c.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(c => c.ContactEmail)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Email_Format_Validation);

            RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

        }

    }
}
