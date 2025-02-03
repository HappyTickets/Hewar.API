using Application.Hewar.Dtos;
using FluentValidation;

namespace Application.Hewar.Validators
{
    internal class CreateHewarServiceValidator : AbstractValidator<CreateHewarServiceDto>
    {
        public CreateHewarServiceValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Description)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
