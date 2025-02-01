using Application.PriceRequests.Dtos.Offers;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    internal class PriceOfferServiceValidator : AbstractValidator<PriceOfferServiceDto>
    {
        internal PriceOfferServiceValidator()
        {
            RuleFor(r => r.ServiceId)
             .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.Quantity)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);


            RuleFor(r => r.DailyCostPerUnit)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.MonthlyCostPerUnit)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.ShiftType)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
               .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);

        }
    }
}
