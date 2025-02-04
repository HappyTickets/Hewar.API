using Application.PriceOffers.Dtos.Services;
using FluentValidation;

namespace Application.PriceOffers.Validators
{
    public class CreateOtherServiceOfferValidator : AbstractValidator<CreateOtherServiceOfferDto>
    {
        public CreateOtherServiceOfferValidator()
        {

            RuleFor(g => g.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.DailyCostPerUnit)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.MonthlyCostPerUnit)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ShiftType) // OPTIONAL but if passed then check is in enum
                .IsInEnum().When(g => g.ShiftType != null)
                .WithState(_ => (int)ValidationMsgs.InvalidValue);

        }
    }
}
