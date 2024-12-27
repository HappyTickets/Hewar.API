using Application.InsuranceAds.Dtos;
using FluentValidation;

namespace Application.InsuranceAds.Validators
{
    public class CreateInsuranceAdOfferMessageDtoValidator: AbstractValidator<CreateInsuranceAdOfferMessageDto>
    {
        public CreateInsuranceAdOfferMessageDtoValidator()
        {
            RuleFor(t => t.Content)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(t => t.InsuranceAdOfferId)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
