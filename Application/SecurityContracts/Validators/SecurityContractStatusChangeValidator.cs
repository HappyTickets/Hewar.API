using Application.SecurityContracts.DTOs;
using FluentValidation;

namespace Application.SecurityContracts.Validators
{
    public class SecurityContractStatusChangeValidator : AbstractValidator<SecurityContractStatusChangeDto>
    {
        public SecurityContractStatusChangeValidator()
        {
            RuleFor(x => x.SecurityContractId)
                .NotNull().WithState(x => ValidationMsgs.RequiredField);

            RuleFor(x => x.Status)
                .IsInEnum().WithState(x => ValidationMsgs.InvalidValue);
        }
    }

}
