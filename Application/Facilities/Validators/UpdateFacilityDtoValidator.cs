using Application.Common.Validators;
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

            RuleFor(x => x.Type)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.CommercialRegistration)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.ActivityType)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.Address)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.ResponsibleName)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(x => x.ResponsiblePhone)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
