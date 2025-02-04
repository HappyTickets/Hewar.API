using Application.Account.Validators;
using Application.AccountManagement.Dtos.Authentication;
using Application.Common.Validators;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class RegisterFacilityRequestValidator : AbstractValidator<RegisterFacilityRequest>
    {

        public RegisterFacilityRequestValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.Type)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.CommercialRegistration)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.ActivityType)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Logo)
           .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.AdminInfo)
             .NotNull().WithState(_ => (int)ValidationMsgs.Required_AdminAccount)
             .SetValidator(new AdminInfoValidator());

            RuleFor(x => x.Address)
             .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
             .SetValidator(new AddressDtoValidator());

        }

    }
}
