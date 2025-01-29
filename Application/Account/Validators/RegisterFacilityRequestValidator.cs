using Application.AccountManagement.Dtos.Authentication;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class RegisterFacilityRequestValidator : AbstractValidator<RegisterFacilityRequest>
    {

        public RegisterFacilityRequestValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.Type)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.CommercialRegistration)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.ActivityType)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }

    }
}
