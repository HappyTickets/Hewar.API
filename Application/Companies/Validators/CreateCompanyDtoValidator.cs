using Application.Companies.Dtos;
using FluentValidation;

namespace Application.Companies.Validators
{
    public class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDto>
    {
        public CreateCompanyDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(c => c.Email)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Password_Format_Validation);

            RuleFor(c => c.Phone)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(c => c.Password)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Password_Validation)
                .Matches(RegexTemplates.Password).WithState(_ => (int)ValidationMsgs.Password_Format_Validation);

            RuleFor(c => c.Address)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
