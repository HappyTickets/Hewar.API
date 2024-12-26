using Application.Guards.Dtos;
using FluentValidation;

namespace Application.Guards.Validators
{
    public class SkillDtoValidator: AbstractValidator<SkillDto>
    {
        public SkillDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(s => s.YearsOfExperience)
                .NotNull().WithMessage(Resource.RequiredField)
                .GreaterThan(-1);
        }
    }
}
