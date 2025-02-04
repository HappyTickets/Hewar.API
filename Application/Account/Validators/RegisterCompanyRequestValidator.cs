using Application.Account.Validators;
using Application.AccountManagement.Dtos.Authentication;
using Application.Common.Validators;
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

            RuleFor(x => x.AdminInfo)
             .NotNull().WithState(_ => (int)ValidationMsgs.Required_AdminAccount)
             .SetValidator(new AdminInfoValidator());

            RuleFor(x => x.Logo)
             .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Address)
             .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
             .SetValidator(new AddressDtoValidator());

        }

    }
}
