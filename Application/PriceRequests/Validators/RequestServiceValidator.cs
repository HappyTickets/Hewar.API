using Application.PriceRequests.Dtos.Requests;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    internal class RequestServiceValidator : AbstractValidator<PriceRequestServiceDto>
    {
        public RequestServiceValidator()
        {
            RuleFor(r => r.ServiceId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
            RuleFor(r => r.Quantity)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.ShiftType)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
               .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);
        }
    }
}
