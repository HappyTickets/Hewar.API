using Application.PriceRequests.Dtos;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    public class CreatePriceRequestFacilityDetailsValidator: AbstractValidator<CreatePriceRequestFacilityDetailsDto>
    {
        public CreatePriceRequestFacilityDetailsValidator()
        {
            RuleFor(d => d.FacilityName)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(d => d.FacilityEmail)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.FacilityPhone)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.FacilityAddress)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.FacilitySize)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.FacilityActivityType)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.FacilityCommercialRegistrationNumber)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.FacilityCommercialRegistrationExpiryDate)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.FacilityLicenseNumber)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.FacilityLicenseExpiryDate)
                .NotEmpty().WithMessage(Resource.RequiredField);
                        
            RuleFor(d => d.RepresentativeName)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.RepresentativeEmail)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.RepresentativePhone)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.RepresentativeNationalId)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.RepresentativeNationality)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.CommissionerName)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.CommissionerEmail)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.CommissionerPhone)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.CommissionerNationalId)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.CommissionerNationality)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(d => d.PriceRequestId)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
