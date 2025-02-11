using Application.PriceRequests.Dtos;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    public class UpdatePriceRequestValidator : AbstractValidator<UpdatePriceRequestDto>
    {
        public UpdatePriceRequestValidator()
        {
            RuleFor(r => r.ContractType)
               .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
               .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(r => r.StartDate)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.EndDate)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);


            RuleFor(r => r.PriceRequestId)
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
