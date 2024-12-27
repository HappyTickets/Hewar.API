using Application.InsuranceAds.Dtos;
using FluentValidation;

namespace Application.InsuranceAds.Validators
{
    public class CreateInsuranceAdOfferDtoValidator: AbstractValidator<CreateInsuranceAdOfferDto>
    {
        public CreateInsuranceAdOfferDtoValidator()
        {
            RuleFor(g => g.InsuranceAdId)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(g => g.Offer)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
