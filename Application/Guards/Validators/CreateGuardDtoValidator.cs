using Application.Guards.Dtos;
using FluentValidation;

namespace Application.Guards.Validators
{
    public class CreateGuardDtoValidator: AbstractValidator<CreateGuardDto>
    {
        public CreateGuardDtoValidator()
        {
            RuleFor(g => g.UserName)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .Must(un => !un.Contains(" ")).WithMessage(Resource.UserName_NoSpaces);

            RuleFor(g => g.FirstName)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.LastName)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(g => g.Phone)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.Password)
                .NotEmpty().WithMessage(Resource.Password_Validation)
                .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation);

            RuleFor(g => g.DateOfBirth)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.NationalId)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.Qualification)
                .NotNull().WithMessage(Resource.RequiredField)
                .IsInEnum().WithMessage(Resource.InvalidValue);

            RuleFor(g => g.City)
                .NotNull().WithMessage(Resource.RequiredField)
                .IsInEnum().WithMessage(Resource.InvalidValue);

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
