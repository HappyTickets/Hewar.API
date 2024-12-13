using Application.Tickets.Dtos;
using AutoMapper;

namespace Application.Tickets.Mappings
{
    internal class TicketsProfile: Profile
    {
        public TicketsProfile()
        {
            CreateMap<CreateTicketMessageDto, TicketMessage>();
            CreateMap<Ticket, TicketDto>();
            CreateMap<TicketMessage, TicketMessageDto>();
        }
    }
}
