using Application.Common.Validators;
using Application.SecurityCertificates.DTOs;
using FluentValidation;

namespace Application.SecurityCertificates.Validators
{
    public class SecurityCertificateCreateValidator : AbstractValidator<SecurityCertificateCreateDto>
    {
        public SecurityCertificateCreateValidator()
        {
            RuleFor(x => x.StartDate)
                .NotNull().WithState(x => ValidationMsgs.RequiredField)
                .LessThan(x => x.EndDate)
                .WithMessage("Start date must be before the end date.")
                .WithState(x => ValidationMsgs.InvalidValue);

            RuleFor(x => x.EndDate)
                .NotNull().WithState(x => ValidationMsgs.RequiredField);

            RuleFor(x => x.SiteArea)
                .NotNull().WithState(x => ValidationMsgs.RequiredField)
                .GreaterThan(0).WithState(x => ValidationMsgs.InvalidValue);

            RuleFor(x => x.AgreedNumberOfSecurityGuards)
                .GreaterThan(0).WithState(x => ValidationMsgs.RequiredField);

            RuleFor(x => x.Address)
                .SetValidator(x => new AddressDtoValidator());

            RuleFor(x => x.ContractDocumentUrl)
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Contract document URL must be a valid URI.")
                .WithState(x => ValidationMsgs.InvalidValue);
        }
    }
}
