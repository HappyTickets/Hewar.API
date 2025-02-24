using Application.Tickets.Dtos;
using Application.Tickets.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class TicketsController(ITicketsService ticketsService) : ApiControllerBase
    {
        [HttpPost("create")]
        [HasPermission(Permissions.CreateTicket)]
        public async Task<IActionResult> CreateTicketAsync([FromBody] CreateTicketDto dto)
            => Result(await ticketsService.CreateTicketAsync(dto));

        [HttpPatch("close")]
        [HasPermission(Permissions.CloseTicket)]
        public async Task<IActionResult> CloseTicketAsync(long ticketId)
           => Result(await ticketsService.CloseTicketAsync(ticketId));

        [HttpGet("getMyReceivedTickets")]
        [HasPermission(Permissions.ViewReceivedTickets)]
        public async Task<IActionResult> GetMyReceivedTicketsAsync()
            => Result(await ticketsService.GetMyReceivedTicketsAsync());

        [HttpGet("getMySentTickets")]
        [HasPermission(Permissions.ViewSentTickets)]
        public async Task<IActionResult> GetMySentTicketsAsync()
            => Result(await ticketsService.GetMySentTicketsAsync());

        [HttpPost("createTicketMessage")]
        [HasPermission(Permissions.CreateTicketMessage)]
        public async Task<IActionResult> CreateTicketMessageAsync(CreateTicketMessageDto dto)
            => Result(await ticketsService.CreateTicketMessageAsync(dto));

        [HttpGet("getTicketMessages")]
        [HasPermission(Permissions.ViewTicketMessages)]
        public async Task<IActionResult> GetTicketMessagesAsync(long ticketId)
            => Result(await ticketsService.GetTicketMessagesAsync(ticketId));
    }
}
