using Application.Guards.Dtos;
using FluentValidation;

namespace Application.Guards.Validators
{
    public class UpdateGuardDtoValidator : AbstractValidator<UpdateGuardDto>
    {
        public UpdateGuardDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .Must(un => !un.Contains(" ")).WithState(_ => (int)ValidationMsgs.UserName_NoSpaces);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.LastName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Email)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Email_Format_Validation);

            RuleFor(x => x.Phone)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Skills)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.NationalId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Qualification)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(g => g.City)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(g => g.Skills)
                .ForEach(b =>
                {
                    b.SetValidator(new SkillDtoValidator());
                });

            RuleFor(g => g.PrevCompanies)
                .ForEach(b =>
                {
                    b.SetValidator(new PrevCompanyDtoValidator());
                });
        }
    }
}
