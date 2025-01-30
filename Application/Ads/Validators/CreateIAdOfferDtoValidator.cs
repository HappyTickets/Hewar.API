using Application.Ads.Dtos;
using FluentValidation;

namespace Application.Ads.Validators
{
    public class CreateIAdOfferDtoValidator : AbstractValidator<CreateAdOfferDto>
    {
        public CreateIAdOfferDtoValidator()
        {
            RuleFor(g => g.AdId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Offer)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
