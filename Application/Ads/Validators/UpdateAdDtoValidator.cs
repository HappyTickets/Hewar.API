using Application.Ads.Dtos.Post;
using FluentValidation;

namespace Application.Ads.Validators
{
    public class UpdateAdDtoValidator : AbstractValidator<UpdateAdDto>
    {
        public UpdateAdDtoValidator()
        {
            RuleFor(g => g.Title)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(g => g.Status)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField)
                .IsInEnum().WithState(_ => (int)ValidationMsgs.InvalidValue);
        }
    }
}
