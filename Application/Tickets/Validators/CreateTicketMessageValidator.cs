using Application.Tickets.Dtos;
using FluentValidation;

namespace Application.Tickets.Validators
{
    public class CreateTicketMessageValidator: AbstractValidator<CreateTicketMessageDto>
    {
        public CreateTicketMessageValidator()
        {
            RuleFor(t => t.Content)
                .NotEmpty().WithMessage(Resource.RequiredField);

            RuleFor(t => t.TicketId)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
