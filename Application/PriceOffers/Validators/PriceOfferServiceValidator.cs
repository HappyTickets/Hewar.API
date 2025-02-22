using Application.PriceOffers.Dtos.Services;
using FluentValidation;

namespace Application.PriceOffers.Validators
{
    internal class PriceOfferServiceValidator : AbstractValidator<CreateServiceCostDto>
    {
        internal PriceOfferServiceValidator()
        {
            RuleFor(r => r.ServiceId)
             .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.Quantity)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField);


            RuleFor(r => r.DailyCostPerUnit)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.MonthlyCostPerUnit)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.ShiftType)
               .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
               .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

        }
    }
}
