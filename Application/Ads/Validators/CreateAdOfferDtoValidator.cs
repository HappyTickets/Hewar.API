using Application.Ads.Dtos.Offers;
using FluentValidation;

namespace Application.Ads.Validators
{
    public class CreateAdOfferDtoValidator : AbstractValidator<CreateAdOfferDto>
    {
        public CreateAdOfferDtoValidator()
        {
            RuleFor(g => g.AdId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ServicesPrice)
                .ForEach(b =>
                {
                    b.SetValidator(new AdServicePriceDtoValidator());
                });
        }
    }
}
