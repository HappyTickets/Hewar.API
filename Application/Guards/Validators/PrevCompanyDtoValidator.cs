using Application.Guards.Dtos;
using FluentValidation;

namespace Application.Guards.Validators
{
    public class PrevCompanyDtoValidator : AbstractValidator<PrevCompanyDto>
    {
        public PrevCompanyDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(s => s.From)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(s => s.To)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
