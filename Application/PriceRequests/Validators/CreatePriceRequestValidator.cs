using Application.PriceRequests.Dtos;
using FluentValidation;
using Localization.ResourceFiles;

namespace Application.PriceRequests.Validators
{
    public class CreatePriceRequestValidator: AbstractValidator<CreatePriceRequestDto>
    {
        public CreatePriceRequestValidator()
        {
            RuleFor(r => r.SecurityRole)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .IsInEnum().WithMessage(Resource.InvalidValue);

            RuleFor(r => r.GuardsCount)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(r => r.WorkShift)
               .NotEmpty().WithMessage(Resource.RequiredField)
               .IsInEnum().WithMessage(Resource.InvalidValue);

            RuleFor(r => r.ContractType)
               .NotEmpty().WithMessage(Resource.RequiredField)
               .IsInEnum().WithMessage(Resource.InvalidValue);

            RuleFor(r => r.StartDate)
               .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(r => r.EndDate)
               .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(r => r.CompanyId)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
