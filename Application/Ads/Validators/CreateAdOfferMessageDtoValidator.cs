using Application.Ads.Dtos;
using FluentValidation;

namespace Application.Ads.Validators
{
    public class CreateAdOfferMessageDtoValidator : AbstractValidator<CreateAdOfferMessageDto>
    {
        public CreateAdOfferMessageDtoValidator()
        {
            RuleFor(t => t.Content)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(t => t.AdOfferId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
