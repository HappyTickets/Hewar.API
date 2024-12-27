using Application.Facilities.Dtos;
using FluentValidation;

namespace Application.Facilities.Validators
{
    public class UpdateFacilityDtoValidator: AbstractValidator<UpdateFacilityDto>
    {
        public UpdateFacilityDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(Resource.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithMessage(Resource.Email_Format_Validation);

            RuleFor(r => r.Phone)
                .NotEmpty().WithMessage(Resource.RequiredField);

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
