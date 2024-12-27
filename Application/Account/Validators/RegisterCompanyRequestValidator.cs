using Application.AccountManagement.Dtos.Authentication;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class RegisterCompanyRequestValidator : AbstractValidator<RegisterCompanyRequest>
    {

        public RegisterCompanyRequestValidator()
        {

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(c => c.Phone)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage(Resource.Password_Validation)
                .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation);

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }

    }
}
