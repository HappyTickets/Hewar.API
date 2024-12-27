using Application.Guards.Dtos;
using FluentValidation;

namespace Application.Guards.Validators
{
    public class PrevCompanyDtoValidator: AbstractValidator<PrevCompanyDto>
    {
        public PrevCompanyDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(s => s.From)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(s => s.To)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
