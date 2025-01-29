using Application.Tickets.Dtos;
using FluentValidation;

namespace Application.Tickets.Validators
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(t => t.Content)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

            RuleFor(t => t.AudienceId)
                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

        }
    }
}
