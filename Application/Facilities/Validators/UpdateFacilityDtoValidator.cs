using Application.Facilities.Dtos;
using FluentValidation;

namespace Application.Facilities.Validators
{
    public class UpdateFacilityDtoValidator : AbstractValidator<UpdateFacilityDto>
    {
        public UpdateFacilityDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(r => r.Email)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Email_Format_Validation);

            RuleFor(r => r.Phone)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Type)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.CommercialRegistration)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.ActivityType)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Address)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.City)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.ResponsibleName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.ResponsiblePhone)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
