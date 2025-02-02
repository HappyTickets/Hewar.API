using Application.Chats.DTOs;
using FluentValidation;

namespace Application.Chats.Validators
{
    public class CreateChatMessageDtoValidator : AbstractValidator<CreateChatMessageDto>
    {
        public CreateChatMessageDtoValidator()
        {
            RuleFor(t => t)
                  .Must(dto => !string.IsNullOrWhiteSpace(dto.Content) ||
                       (dto.Medias != null && dto.Medias.Any()))
                  .WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(t => t.ChatId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
