using Application.Tickets.Dtos;
using Application.Tickets.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class TicketsController(ITicketsService ticketsService) : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateTicketAsync([FromBody] CreateTicketDto dto)
            => Result(await ticketsService.CreateTicketAsync(dto));

        [HttpPatch("close")]
        public async Task<IActionResult> CloseTicketAsync(long ticketId)
           => Result(await ticketsService.CloseTicketAsync(ticketId));

        [HttpGet("getMyReceivedTickets")]
        public async Task<IActionResult> GetMyReceivedTicketsAsync()
            => Result(await ticketsService.GetMyReceivedTicketsAsync());

        [HttpGet("getMySentTickets")]
        public async Task<IActionResult> GetMySentTicketsAsync()
            => Result(await ticketsService.GetMySentTicketsAsync());

        [HttpPost("createTicketMessage")]
        public async Task<IActionResult> CreateTicketMessageAsync(CreateTicketMessageDto dto)
            => Result(await ticketsService.CreateTicketMessageAsync(dto));

        [HttpGet("getTicketMessages")]
        public async Task<IActionResult> GetTicketMessagesAsync(long ticketId)
            => Result(await ticketsService.GetTicketMessagesAsync(ticketId));
    }
}
