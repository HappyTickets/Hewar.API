using Application.Tickets.Dtos;
using AutoMapper;
using Domain.Events.Tickets;

namespace Application.Tickets.Service
{
    internal class TicketsService : ITicketsService
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
            var ticket = new Ticket
            {
                Title = dto.Title,
                OpenedDate = DateTimeOffset.UtcNow,
                ClosedDate = null,
                Status = TicketStatus.Opened,
                AudienceId = dto.AudienceId,
                AudienceType = dto.AudienceType,
                IssuerId = _currentUser.Id!.Value,
                IssuerType = _currentUser.Type!.Value,
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

            return Result<Empty>.Success(Empty.Default, SuccessCodes.TicketCreated);
        }

        public async Task<Result<Empty>> CreateTicketMessageAsync(CreateTicketMessageDto dto)
        {
            var ticket = await _ufw.Tickets.GetByIdAsync(dto.TicketId);

            if (ticket == null)
                return new NotFoundError();

            if (ticket.Status == TicketStatus.Closed)
                return new ConflictError(ErrorCodes.TicketClosed);

            var message = _mapper.Map<TicketMessage>(dto);

            message.SentDate = DateTimeOffset.UtcNow;
            message.SenderId = _currentUser.Id!.Value;
            message.SenderType = _currentUser.Type!.Value;
            message.Ticket = ticket;

            message.AddDomainEvent(new TicketMessageCreated(message));
            _ufw.TicketMessages.Create(message);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateTicketMessage);
        }

        public async Task<Result<Empty>> CloseTicketAsync(long ticketId)
        {
            var ticket = await _ufw.Tickets.GetByIdAsync(ticketId);

            if (ticket == null)
                return new NotFoundError();

            if (ticket.AudienceId != _currentUser.Id || ticket.AudienceType != _currentUser.Type)
                return new ForbiddenError(ErrorCodes.TicketAudienceError);

            if (ticket.Status == TicketStatus.Closed)
                return new ConflictError(ErrorCodes.TicketClosed);

            ticket.Status = TicketStatus.Closed;
            ticket.ClosedDate = DateTimeOffset.UtcNow;

            ticket.AddDomainEvent(new TicketClosed(ticket));
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.TicketClosed);
        }

        public async Task<Result<TicketDto[]>> GetMyReceivedTicketsAsync()
        {
            var tickets = await _ufw.Tickets
                .FilterAsync(t => t.AudienceId == _currentUser.Id && t.AudienceType == _currentUser.Type);

            var ticketDto = _mapper.Map<TicketDto[]>(tickets);
            return Result<TicketDto[]>.Success(ticketDto, SuccessCodes.GetMyReceivedTickets);

        }

        public async Task<Result<TicketDto[]>> GetMySentTicketsAsync()
        {
            var tickets = await _ufw.Tickets
                .FilterAsync(t => t.IssuerId == _currentUser.Id && t.IssuerType == _currentUser.Type);

            var ticketDto = _mapper.Map<TicketDto[]>(tickets);
            return Result<TicketDto[]>.Success(ticketDto, SuccessCodes.GetMySentTickets);

        }

        public async Task<Result<TicketMessageDto[]>> GetTicketMessagesAsync(long ticketId)
        {
            var messages = await _ufw.TicketMessages
                .FilterAsync(t => t.TicketId == ticketId);

            var ticketMessageDto = _mapper.Map<TicketMessageDto[]>(messages);
            return Result<TicketMessageDto[]>.Success(ticketMessageDto, SuccessCodes.GetTicketMessages);
        }
    }

}
