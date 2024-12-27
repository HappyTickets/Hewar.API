using Application.Tickets.Dtos;
using FluentValidation;

namespace Application.Tickets.Validators
{
    public class CreateTicketValidator: AbstractValidator<CreateTicketDto>
    {
        public CreateTicketValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage(Resource.RequiredField); 
            
            RuleFor(t => t.Content)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(t => t.AudienceId)
                .NotEmpty().WithMessage(Resource.RequiredField);
            
            RuleFor(t => t.AudienceType)
                .NotEmpty().WithMessage(Resource.RequiredField);
        }
    }
}
