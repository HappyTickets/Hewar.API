using Application.PriceRequests.Dtos.Services;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    public class CreateOtherServiceValidator : AbstractValidator<CreateOtherServiceDto>
    {
        public CreateOtherServiceValidator()
        {

            RuleFor(g => g.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);


            RuleFor(g => g.ShiftType) // OPTIONAL but if passed then check is in enum
                .IsInEnum().When(g => g.ShiftType != null)
                .WithState(_ => (int)ValidationMsgs.InvalidValue);

        }
    }
}
