using Application.PriceOffers.Dtos;
using FluentValidation;

namespace Application.PriceOffers.Validators
{
    internal class UpdatePriceOfferValidator : AbstractValidator<UpdatePriceOfferDto>
    {
        internal UpdatePriceOfferValidator()
        {
            RuleFor(r => r.PriceOfferId)
             .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r)
                      .Must(r => r.Services != null && r.Services.Any() || r.OtherServices != null && r.OtherServices.Any())
                      .WithState(_ => (int)ValidationMsgs.RequiredField)
                      .WithMessage("You must provide at least Services or OtherServices.");


            RuleFor(r => r.ContractType)
            .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
            .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(r => r.StartDate)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.EndDate)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleForEach(r => r.Services)
                .SetValidator(new PriceOfferServiceValidator())
                .When(r => r.Services != null && r.Services.Any());

            RuleForEach(r => r.OtherServices)
                .SetValidator(new CreateOtherServiceOfferValidator())
                .When(r => r.OtherServices != null && r.OtherServices.Any());
        }

    }
}
