using Application.Companies.Dtos;
using FluentValidation;

namespace Application.Companies.Validators
{
    public class UpdateCompanyDtoValidator : AbstractValidator<UpdateCompanyDto>
    {
        public UpdateCompanyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Email)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Email_Format_Validation);

            RuleFor(x => x.Phone)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Address)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
