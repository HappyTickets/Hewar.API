using Application.Tickets.Dtos;
using FluentValidation;

namespace Application.Tickets.Validators
{
    public class CreateTicketMessageValidator: AbstractValidator<CreateTicketMessageDto>
    {
        public CreateTicketMessageValidator()
        {
            RuleFor(t => t.Content)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(t => t.TicketId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
        }
    }
}
