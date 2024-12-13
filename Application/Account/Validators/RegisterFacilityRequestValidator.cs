using Application.AccountManagement.Dtos.Authentication;
using FluentValidation;
using Localization.ResourceFiles;

namespace Application.Authorization.Validators
{
    public class RegisterFacilityRequestValidator : AbstractValidator<RegisterFacilityRequest>
    {

        public RegisterFacilityRequestValidator()
        {
            RuleFor(r => r.Name)
            .NotEmpty().WithMessage(Resource.RequiredField);


            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(r => r.Phone)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(Resource.Password_Validation)
                .Matches(RegexTemplates.Password).WithMessage(Resource.Password_Format_Validation);

            RuleFor(x => x.Type)
            .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(x => x.CommercialRegistration)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(x => x.ActivityType)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(x => x.Address)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(x => x.City)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(x => x.ResponsibleName)
            .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(x => x.ResponsiblePhone)
            .NotEmpty().WithMessage(Resource.RequiredField);
        }

    }
}
