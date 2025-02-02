using Application.Ads.Dtos.AdServices;
using FluentValidation;

namespace Application.Ads.Validators
{
    internal class AdServiceDtoValidator : AbstractValidator<AdServiceDto>
    {
        public AdServiceDtoValidator()
        {
            RuleFor(g => g.ServiceId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Quantity)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ShiftType)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);


        }
    }
}
