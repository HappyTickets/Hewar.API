using Application.Guards.Dtos;
using FluentValidation;

namespace Application.Guards.Validators
{
    public class UpdateGuardDtoValidator : AbstractValidator<UpdateGuardDto>
    {
        public UpdateGuardDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .Must(un => !un.Contains(" ")).WithMessage(Resource.UserName_NoSpaces);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.Skills)
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
