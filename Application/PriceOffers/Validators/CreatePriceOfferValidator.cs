using Application.PriceOffers.Dtos;
using Application.PriceRequests.Validators;
using FluentValidation;

namespace Application.PriceOffers.Validators
{
    internal class CreatePriceOfferValidator : AbstractValidator<CreatePriceOfferDto>
    {
        internal CreatePriceOfferValidator()
        {
            RuleFor(r => r.PriceRequestId)
             .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r)
              .Must(r => r.Services != null && r.Services.Any() ||
              r.OtherServices != null && r.OtherServices.Any())
              .WithState(_ => (int)ValidationMsgs.RequiredField)
              .WithMessage("You must provide at least Services or OtherServices.");

            RuleForEach(r => r.Services)
                .SetValidator(new PriceOfferServiceValidator())
                .When(r => r.Services != null && r.Services.Any());

            RuleForEach(r => r.OtherServices)
                .SetValidator(new CreateOtherServiceOfferValidator())
                .When(r => r.OtherServices != null && r.OtherServices.Any());

        }

    }
}
