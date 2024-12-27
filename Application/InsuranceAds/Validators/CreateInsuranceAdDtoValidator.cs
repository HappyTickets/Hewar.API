using Application.InsuranceAds.Dtos;
using FluentValidation;

namespace Application.InsuranceAds.Validators
{
    public class CreateInsuranceAdDtoValidator: AbstractValidator<CreateInsuranceAdDto>
    {
        public CreateInsuranceAdDtoValidator()
        {
            RuleFor(g => g.SecurityRole)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .IsInEnum().WithMessage(Resource.InvalidValue);
            
            RuleFor(g => g.GuardsCount)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.WorkShift)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .IsInEnum().WithMessage(Resource.InvalidValue);
            
            RuleFor(g => g.ContractType)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .IsInEnum().WithMessage(Resource.InvalidValue);
            
            RuleFor(g => g.StartDate)
                .NotEmpty().WithMessage(Resource.RequiredField); 
            
            RuleFor(g => g.EndDate)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(g => g.Description)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.Status)
                .NotEmpty().WithMessage(Resource.RequiredField)
                .IsInEnum().WithMessage(Resource.InvalidValue);
        }
    }
}
