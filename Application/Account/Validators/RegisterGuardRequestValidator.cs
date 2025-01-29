using Application.AccountManagement.Dtos.Authentication;
using Application.Guards.Validators;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class RegisterGuardRequestValidator : AbstractValidator<RegisterGuardRequest>
    {
        public RegisterGuardRequestValidator()
        {
            RuleFor(g => g.UserName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .Must(un => !un.Contains(" "))
                .WithState(_ => (int)ValidationMsgs.UserName_NoSpaces);

            RuleFor(g => g.FirstName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.LastName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Email)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Email_Format_Validation);

            RuleFor(g => g.Phone)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Password)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Password_Validation)
                .Matches(RegexTemplates.Password).WithState(_ => (int)ValidationMsgs.Password_Format_Validation);

            RuleFor(g => g.DateOfBirth)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.NationalId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Qualification)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(g => g.City)
               .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
                .WithState(_ => (int)ValidationMsgs.InvalidValue);

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
