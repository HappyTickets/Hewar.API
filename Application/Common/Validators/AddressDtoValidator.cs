using Application.Common.Dtos;
using FluentValidation;

namespace Application.Common.Validators
{
    internal class AddressDtoValidator : AbstractValidator<AddressDto>
    {

        internal AddressDtoValidator()
        {
            RuleFor(address => address.Street)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .MaximumLength(100).WithState(_ => (int)ValidationMsgs.MaxLengthExceeded);

            RuleFor(address => address.City)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .MaximumLength(50).WithState(_ => (int)ValidationMsgs.MaxLengthExceeded);

            RuleFor(address => address.State)
               .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .MaximumLength(50).WithState(_ => (int)ValidationMsgs.MaxLengthExceeded);

            RuleFor(address => address.PostalCode)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .Matches(@"^\d{5}$").WithState(_ => (int)ValidationMsgs.MaxLengthExceeded);
        }

    }
}
