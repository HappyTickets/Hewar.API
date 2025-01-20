using Application.PriceRequests.Dtos;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    public class CreatePriceRequestValidator : AbstractValidator<CreatePriceRequestDto>
    {
        public CreatePriceRequestValidator()
        {
            RuleFor(r => r.SecurityRole)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(r => r.GuardsCount)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.WorkShift)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
               .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(r => r.ContractType)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
               .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(r => r.StartDate)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.EndDate)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.Description)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.CompanyId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
