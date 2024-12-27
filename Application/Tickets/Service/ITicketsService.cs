using Application.Tickets.Dtos;

namespace Application.Tickets.Service
{
    public interface ITicketsService
    {
        Task<Result<Empty>> CloseTicketAsync(long ticketId);
        Task<Result<Empty>> CreateTicketMessageAsync(CreateTicketMessageDto dto);
        Task<Result<Empty>> CreateTicketAsync(CreateTicketDto dto);
        Task<Result<TicketMessageDto[]>> GetTicketMessagesAsync(long ticketId);
        Task<Result<TicketDto[]>> GetMyReceivedTicketsAsync();
        Task<Result<TicketDto[]>> GetMySentTicketsAsync();
    }
}
