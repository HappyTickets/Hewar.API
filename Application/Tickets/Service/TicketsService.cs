using Application.Tickets.Dtos;
using AutoMapper;
using Domain.Events.Tickets;

namespace Application.Tickets.Service
{
    internal class TicketsService: ITicketsService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public TicketsService(IUnitOfWorkService ufw, ICurrentUserService currentUser, IMapper mapper)
        {
            _ufw = ufw;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<Result<Empty>> CreateTicketAsync(CreateTicketDto dto)
        {
            var priceRequest = await _ufw.PriceRequests.GetByIdAsync(dto.PriceRequestId);

            if (priceRequest == null)
                return new NotFoundException();

            if (priceRequest.Status != RequestStatus.Pending)
                return new ConflictException(Resource.OnlyPendingRequests);

            var ticket = new Ticket
            {
                Title = dto.Title,
                OpenedDate = DateTimeOffset.UtcNow,
                ClosedDate = null,
                Status = TicketStatus.Opened,
                PriceRequestId = dto.PriceRequestId,
                PriceRequest = priceRequest,
                Messages = 
                [
                    new TicketMessage
                    {
                        Content = dto.Content,
                        Medias = _mapper.Map<Media[]>(dto.Medias),
                        SentDate = DateTimeOffset.UtcNow,
                        SenderId = _currentUser.Id!.Value,
                        SenderType =_currentUser.Type!.Value
                    }
                ]
            };

            ticket.AddDomainEvent(new TicketCreated(ticket));
            _ufw.Tickets.Create(ticket);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> CreateMessageAsync(CreateTicketMessageDto dto)
        {
            var ticket = await _ufw.Tickets.GetByIdAsync(dto.TicketId, ["PriceRequest"]);

            if (ticket == null)
                return new NotFoundException();

            if (ticket.Status == TicketStatus.Closed)
                return new ConflictException(Resource.OnlyOpenedTickets);

            var message = _mapper.Map<TicketMessage>(dto);

            message.SentDate = DateTimeOffset.UtcNow;
            message.SenderId = _currentUser.Id!.Value;
            message.SenderType = _currentUser.Type!.Value;
            message.Ticket = ticket;

            message.AddDomainEvent(new TicketMessageCreated(message));
            _ufw.TicketMessages.Create(message);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        } 

        public async Task<Result<Empty>> CloseTicketAsync(long ticketId)
        {
            var ticket = await _ufw.Tickets.GetByIdAsync(ticketId, ["PriceRequest"]);

            if(ticket == null)
                return new NotFoundException();

            if (ticket.Status == TicketStatus.Closed)
                return new ConflictException(Resource.ClosedTicketError);

            ticket.Status = TicketStatus.Closed;
            ticket.ClosedDate = DateTimeOffset.UtcNow;

            ticket.AddDomainEvent(new TicketClosed(ticket));
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<TicketDto[]>> GetTicketsAsync(long priceRequestId)
        {
            var tickets = await _ufw.Tickets
                .FilterAsync(t=>t.PriceRequestId == priceRequestId);

            return _mapper.Map<TicketDto[]>(tickets);
        }

        public async Task<Result<TicketMessageDto[]>> GetMessagesAsync(long ticketId)
        {
            var messages = await _ufw.TicketMessages
                .FilterAsync(t => t.TicketId == ticketId);

            return _mapper.Map<TicketMessageDto[]>(messages);
        }
    }

}
