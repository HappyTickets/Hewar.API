using Application.AccountManagement.Dtos.Authentication;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class RegisterCompanyRequestValidator : AbstractValidator<RegisterCompanyRequest>
    {

        public RegisterCompanyRequestValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Resource.Password_Validation)
                .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation);

            RuleFor(x => x.Address)
           .NotEmpty().WithMessage(Resource.RequiredField);
        }

    }
}
