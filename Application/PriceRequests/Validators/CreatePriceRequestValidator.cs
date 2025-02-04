using Application.PriceRequests.Dtos;
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

            RuleFor(r => r)
                      .Must(r => (r.Services != null && r.Services.Any()) ||
                      (r.OtherServices != null && r.OtherServices.Any()))
                      .WithState(_ => (int)ValidationMsgs.RequiredField)
                      .WithMessage("You must provide at least Services or OtherServices.");

            RuleForEach(r => r.Services)
                .SetValidator(new RequestServiceValidator())
                .When(r => r.Services != null && r.Services.Any());

            RuleForEach(r => r.OtherServices)
                .SetValidator(new CreateOtherServiceValidator())
                .When(r => r.OtherServices != null && r.OtherServices.Any());

        }
    }
}
