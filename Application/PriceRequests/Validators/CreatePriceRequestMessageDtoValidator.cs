using Application.PriceRequests.Dtos;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    public class CreatePriceRequestMessageDtoValidator: AbstractValidator<CreatePriceRequestMessageDto>
    {
        public CreatePriceRequestMessageDtoValidator()
        {
            RuleFor(t => t.Content)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(t => t.PriceRequestId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
