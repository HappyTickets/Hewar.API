using Application.Chats.DTOs;
using FluentValidation;

namespace Application.PriceRequests.Validators
{
    public class CreateChatMessageDtoValidator : AbstractValidator<CreateChatMessageDto>
    {
        public CreateChatMessageDtoValidator()
        {
            RuleFor(t => t.Content)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(t => t.ChatId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
