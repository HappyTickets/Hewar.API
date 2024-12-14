using Application.Tickets.Dtos;

namespace Application.Tickets.Service
{
    public interface ITicketsService
    {
        Task<Result<Empty>> CloseTicketAsync(long ticketId);
        Task<Result<Empty>> CreateMessageAsync(CreateTicketMessageDto dto);
        Task<Result<Empty>> CreateTicketAsync(CreateTicketDto dto);
        Task<Result<TicketMessageDto[]>> GetMessagesAsync(long ticketId);
        Task<Result<TicketDto[]>> GetTicketsAsync(long priceRequestId);
    }
}
