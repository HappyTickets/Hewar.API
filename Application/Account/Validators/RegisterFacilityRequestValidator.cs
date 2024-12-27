using Application.AccountManagement.Dtos.Authentication;
using FluentValidation;
using Localization.ResourceFiles;

namespace Application.Authorization.Validators
{
    public class RegisterFacilityRequestValidator : AbstractValidator<RegisterFacilityRequest>
    {

        public RegisterFacilityRequestValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(f => f.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(f => f.Phone)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(f => f.Password)
                .NotEmpty().WithMessage(Resource.Password_Validation)
                .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation);

            RuleFor(f => f.Type)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(f => f.CommercialRegistration)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(f => f.ActivityType)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(f => f.Address)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(f => f.City)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(f => f.ResponsibleName)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(f => f.ResponsiblePhone)
            .NotEmpty().WithMessage(Resource.RequiredField);
        }

    }
}
