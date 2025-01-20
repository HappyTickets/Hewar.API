using Application.Guards.Dtos;
using FluentValidation;

namespace Application.Guards.Validators
{
    public class SkillDtoValidator: AbstractValidator<SkillDto>
    {
        public SkillDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(s => s.YearsOfExperience)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
                .GreaterThan(-1);
        }
    }
}
