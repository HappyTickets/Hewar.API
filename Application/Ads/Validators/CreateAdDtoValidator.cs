using Application.Ads.Dtos;
using FluentValidation;

namespace Application.Ads.Validators
{
    public class CreateAdDtoValidator : AbstractValidator<CreateAdDto>
    {
        public CreateAdDtoValidator()
        {
            RuleFor(g => g.SecurityRole)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(g => g.GuardsCount)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.WorkShift)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(g => g.ContractType)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);



            RuleFor(g => g.StartDate)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.EndDate)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Description)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Status)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);
        }
    }
}
