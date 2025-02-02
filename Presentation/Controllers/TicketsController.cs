using Application.Tickets.Dtos;
using Application.Tickets.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class TicketsController : ApiControllerBase
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateTicketAsync(CreateTicketDto dto)
            => Result(await _ticketsService.CreateTicketAsync(dto));

        [HttpPatch("closeTicket")]
        public async Task<IActionResult> CloseTicketAsync(long ticketId)
           => Result(await _ticketsService.CloseTicketAsync(ticketId));

        [HttpGet("getMyReceivedTickets")]
        public async Task<IActionResult> GetMyReceivedTicketsAsync()
            => Result(await _ticketsService.GetMyReceivedTicketsAsync());

        [HttpGet("getMySentTickets")]
        public async Task<IActionResult> GetMySentTicketsAsync()
            => Result(await _ticketsService.GetMySentTicketsAsync());

        [HttpPost("createTicketMessage")]
        public async Task<IActionResult> CreateTicketMessageAsync(CreateTicketMessageDto dto)
            => Result(await _ticketsService.CreateTicketMessageAsync(dto));

        [HttpGet("getTicketMessages")]
        public async Task<IActionResult> GetTicketMessagesAsync(long ticketId)
            => Result(await _ticketsService.GetTicketMessagesAsync(ticketId));
    }
}
