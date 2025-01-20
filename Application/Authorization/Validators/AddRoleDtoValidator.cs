using Application.Authorization.DTOs.Request;
using FluentValidation;

namespace Application.Authorization.Validators
{
    public class AddRoleDtoValidator : AbstractValidator<AddRoleDto>
    {
        public AddRoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.InvalidName)
                .Matches(@"^[a-zA-Z\u0600-\u06FF\s]+$").WithState(_ => (int)ValidationMsgs.InvalidName);

            RuleFor(r => r.Permissions)
                .ForEach(b =>
                {
                    b.IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);
                });
        }
    }
}
