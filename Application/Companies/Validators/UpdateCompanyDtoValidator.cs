using Application.Companies.Dtos;
using FluentValidation;

namespace Application.Companies.Validators
{
    public class UpdateCompanyDtoValidator: AbstractValidator<UpdateCompanyDto>
    {
        public UpdateCompanyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
