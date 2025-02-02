using Application.Ads.Dtos.Post;
using FluentValidation;

namespace Application.Ads.Validators
{

    public class CreateAdDtoValidator : AbstractValidator<CreateAdDto>
    {
        public CreateAdDtoValidator()
        {

            RuleFor(g => g.Title)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Description)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
            RuleFor(ad => ad.Services)
                .ForEach(b =>
                {
                    b.SetValidator(new AdServiceDtoValidator());
                });
        }
    }
}
