using Application.Authorization.DTOs.Request;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class EditRoleDtoValidator : AbstractValidator<EditRoleDto>
    {
        public EditRoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.InvalidName)
                .Matches(@"^[a-zA-Z\u0600-\u06FF]+$").WithState(_ => (int)ValidationMsgs.InvalidValue);

            RuleFor(r => r.Permissions)
                .ForEach(b =>
                {
                    b.IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);
                });
        }
    }

}
