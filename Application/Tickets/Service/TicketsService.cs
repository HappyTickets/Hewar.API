using Application.Tickets.Dtos;
using AutoMapper;
using Domain.Events.Tickets;

namespace Application.Tickets.Service
{
    internal class TicketsService(IUnitOfWorkService ufw, ICurrentUserService currentUser, IMapper mapper) : ITicketsService
    {
        public async Task<Result<Empty>> CreateTicketAsync(CreateTicketDto dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                OpenedDate = DateTimeOffset.UtcNow,
                ClosedDate = null,
                Status = TicketStatus.Opened,
                AudienceId = dto.AudienceId,
                IssuerId = currentUser.UserId!.Value,
                Messages =
                [
                    new TicketMessage
                    {
                        Content = dto.Content,
                        Medias = mapper.Map<Media[]>(dto.Medias),
                        SentDate = DateTimeOffset.UtcNow,
                        SenderId = currentUser.UserId!.Value
                    }
                ]
            };

            ticket.AddDomainEvent(new TicketCreated(ticket));
            await ufw.GetRepository<Ticket>().CreateAsync(ticket);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.TicketCreated);
        }

        public async Task<Result<Empty>> CreateTicketMessageAsync(CreateTicketMessageDto dto)
        {
            var ticket = await ufw.GetRepository<Ticket>().GetByIdAsync(dto.TicketId);

            if (ticket == null)
                return new NotFoundError();

            if (ticket.Status == TicketStatus.Closed)
                return new ConflictError(ErrorCodes.TicketClosed);

            var message = mapper.Map<TicketMessage>(dto);

            message.SentDate = DateTimeOffset.UtcNow;
            message.SenderId = currentUser.UserId!.Value;
            message.TicketId = ticket.Id;

            message.AddDomainEvent(new TicketMessageCreated(message));
            ufw.GetRepository<TicketMessage>().Create(message);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CreateTicketMessage);
        }

        public async Task<Result<Empty>> CloseTicketAsync(long ticketId)
        {
            var ticket = await ufw.GetRepository<Ticket>().GetByIdAsync(ticketId);

            if (ticket == null)
                return new NotFoundError();

            if (ticket.AudienceId != currentUser.UserId)
                return new ForbiddenError(ErrorCodes.TicketAudienceError);

            if (ticket.Status == TicketStatus.Closed)
                return new ConflictError(ErrorCodes.TicketClosed);

            ticket.Status = TicketStatus.Closed;
            ticket.ClosedDate = DateTimeOffset.UtcNow;

            ticket.AddDomainEvent(new TicketClosed(ticket));
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.TicketClosed);
        }

        public async Task<Result<TicketDto[]>> GetMyReceivedTicketsAsync()
        {
            var tickets = await ufw.GetRepository<Ticket>()
                .FilterAsync(t => t.AudienceId == currentUser.UserId);

            var ticketDto = mapper.Map<TicketDto[]>(tickets);
            return Result<TicketDto[]>.Success(ticketDto, SuccessCodes.GetMyReceivedTickets);

        }

        public async Task<Result<TicketDto[]>> GetMySentTicketsAsync()
        {
            var tickets = await ufw.GetRepository<Ticket>()
                .FilterAsync(t => t.IssuerId == currentUser.UserId);

            var ticketDto = mapper.Map<TicketDto[]>(tickets);
            return Result<TicketDto[]>.Success(ticketDto, SuccessCodes.GetMySentTickets);

        }

        public async Task<Result<TicketMessageDto[]>> GetTicketMessagesAsync(long ticketId)
        {
            var messages = await ufw.GetRepository<TicketMessage>()
                .FilterAsync(t => t.TicketId == ticketId);

            var ticketMessageDto = mapper.Map<TicketMessageDto[]>(messages);
            return Result<TicketMessageDto[]>.Success(ticketMessageDto, SuccessCodes.GetTicketMessages);
        }
    }

}
