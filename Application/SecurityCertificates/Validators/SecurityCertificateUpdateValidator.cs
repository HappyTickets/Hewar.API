using Application.Common.Validators;
using Application.SecurityCertificates.DTOs;
using FluentValidation;

namespace Application.SecurityCertificates.Validators;
public class SecurityCertificateUpdateValidator : AbstractValidator<SecurityCertificateUpdateDto>
{
    public SecurityCertificateUpdateValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithState(x => ValidationMsgs.RequiredField);

        RuleFor(x => x.StartDate)
            .NotNull().WithState(x => ValidationMsgs.RequiredField)
            .LessThan(x => x.EndDate)
            .WithMessage("Start date must be before the end date.");

        RuleFor(x => x.SiteArea)
            .NotNull().WithState(x => ValidationMsgs.RequiredField)
            .GreaterThan(0).WithState(x => ValidationMsgs.RequiredField);

        RuleFor(x => x.AgreedNumberOfSecurityGuards)
            .GreaterThan(0).WithState(x => ValidationMsgs.RequiredField);

        RuleFor(x => x.ContractDocumentUrl)
            .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Contract document URL must be a valid URI.")
            .WithState(x => ValidationMsgs.InvalidValue);

        RuleFor(x => x.Address)
            .SetValidator(x => new AddressDtoValidator());
    }
}
