using Application.InsuranceAds.Dtos;
using FluentValidation;

namespace Application.InsuranceAds.Validators
{
    public class CreateInsuranceAdOfferMessageDtoValidator: AbstractValidator<CreateInsuranceAdOfferMessageDto>
    {
        public CreateInsuranceAdOfferMessageDtoValidator()
        {
            RuleFor(t => t.Content)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(t => t.InsuranceAdOfferId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
