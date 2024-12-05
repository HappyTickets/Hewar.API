using Application.Tenants.Dtos;
using FluentValidation;

namespace Application.Tenants.Validators
{
    public class TenantBriefDtoValidator: AbstractValidator<TenantBriefDto>
    {
        public TenantBriefDtoValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .MinimumLength(10);
        }
    }
}
