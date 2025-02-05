using Application.PriceRequests.Dtos.Services;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    internal class RequestServiceValidator : AbstractValidator<ServiceRequestDto>
    {
        public RequestServiceValidator()
        {
            RuleFor(r => r.ServiceId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
            RuleFor(r => r.Quantity)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.ShiftType)
               .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
               .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);
        }
    }
}
