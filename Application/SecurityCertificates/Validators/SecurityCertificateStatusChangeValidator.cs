using Application.SecurityCertificates.DTOs;
using FluentValidation;

namespace Application.SecurityCertificates.Validators
{
    public class SecurityCertificateStatusChangeValidator : AbstractValidator<SecurityCertificateStatusChangeDto>
    {
        public SecurityCertificateStatusChangeValidator()
        {
            RuleFor(x => x.SecurityCertificateId)
                .NotNull().WithState(x => ValidationMsgs.RequiredField);

            RuleFor(x => x.Status)
                .IsInEnum().WithState(x => ValidationMsgs.InvalidValue);
        }
    }

}
