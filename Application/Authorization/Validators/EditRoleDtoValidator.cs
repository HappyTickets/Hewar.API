using Application.Authorization.DTOs.Request;
using FluentValidation;
using Localization.ResourceFiles;

namespace Application.Authorization.Validators
{
    public class EditRoleDtoValidator : AbstractValidator<EditRoleDto>
    {
        public EditRoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(Resource.InvalidName)
                .Matches(@"^[a-zA-Z\u0600-\u06FF]+$").WithMessage(Resource.InvalidName);

            RuleFor(r => r.Permissions)
                .ForEach(b =>
                {
                    b.IsEnumName(typeof(Permissions));
                });
        }
    }

}
