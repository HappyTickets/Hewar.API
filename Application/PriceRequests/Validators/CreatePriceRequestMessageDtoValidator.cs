using Application.PriceRequests.Dtos;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    public class CreatePriceRequestMessageDtoValidator: AbstractValidator<CreatePriceRequestMessageDto>
    {
        public CreatePriceRequestMessageDtoValidator()
        {
            RuleFor(t => t.Content)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(t => t.PriceRequestId)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
