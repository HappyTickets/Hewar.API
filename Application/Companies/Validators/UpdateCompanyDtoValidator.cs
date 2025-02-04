using Application.Common.Validators;
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

            RuleFor(g => g.RegistrationNumber)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Email_Format_Validation);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.TaxId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Logo)
              .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Address)
                 .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
                 .SetValidator(new AddressDtoValidator());

        }
    }
}
