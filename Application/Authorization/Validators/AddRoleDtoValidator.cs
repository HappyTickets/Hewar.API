using Application.Authorization.DTOs.Request;
using FluentValidation;
using Localization.ResourceFiles;

namespace Application.Authorization.Validators
{
    public class AddRoleDtoValidator : AbstractValidator<AddRoleDto>
    {
        public AddRoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(Resource.InvalidName)
                .Matches(@"^[a-zA-Z\u0600-\u06FF\s]+$").WithMessage(Resource.InvalidName);
        }
    }
}
