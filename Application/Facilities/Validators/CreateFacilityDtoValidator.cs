using Application.Facilities.Dtos;
using FluentValidation;

namespace Application.Facilities.Validators
{
    public class CreateFacilityDtoValidator : AbstractValidator<CreateFacilityDto>
    {
        public CreateFacilityDtoValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.ImageUrl)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.Email)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Email_Required_Validation)
                .Matches(RegexTemplates.Email).WithState(_ => (int)ValidationMsgs.Email_Format_Validation);

            RuleFor(f => f.Phone)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.Password)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.Password_Validation)
                .Matches(RegexTemplates.Password).WithState(_ => (int)ValidationMsgs.Password_Format_Validation);

            RuleFor(f => f.Type)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.CommercialRegistration)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.ActivityType)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.Address)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.City)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.ResponsibleName)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(f => f.ResponsiblePhone)
            .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
