using Application.PriceRequests.Dtos.Requests;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    public class CreatePriceRequestValidator : AbstractValidator<CreatePriceRequestDto>
    {
        public CreatePriceRequestValidator()
        {
            RuleFor(r => r.ContractType)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
               .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(r => r.StartDate)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.EndDate)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.Notes)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.CompanyId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.RequestedServices)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .ForEach(r => r.SetValidator(new RequestServiceValidator()));
        }
    }
}
