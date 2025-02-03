using Application.PriceRequests.Dtos.Offers;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    internal class CreatePriceOfferValidator : AbstractValidator<CreatePriceOfferDto>
    {
        internal CreatePriceOfferValidator()
        {
            RuleFor(r => r.PriceRequestId)
             .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.Services)
             .ForEach(s =>
             {
                 s.SetValidator(new PriceOfferServiceValidator());
             });

        }

    }
}
