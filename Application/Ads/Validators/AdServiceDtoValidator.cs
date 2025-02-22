using Application.Ads.Dtos.AdServices.Req;
using FluentValidation;

namespace Application.Ads.Validators
{
    internal class AdServiceDtoValidator : AbstractValidator<SelectHewarServiceDto>
    {
        public AdServiceDtoValidator()
        {
            RuleFor(g => g.ServiceId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Quantity)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ShiftType)
                .NotNull().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);


        }
    }
}
