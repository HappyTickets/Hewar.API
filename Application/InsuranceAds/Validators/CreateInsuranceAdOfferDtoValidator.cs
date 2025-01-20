using Application.InsuranceAds.Dtos;
using FluentValidation;

namespace Application.InsuranceAds.Validators
{
    public class CreateInsuranceAdOfferDtoValidator: AbstractValidator<CreateInsuranceAdOfferDto>
    {
        public CreateInsuranceAdOfferDtoValidator()
        {
            RuleFor(g => g.InsuranceAdId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
            
            RuleFor(g => g.Offer)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
