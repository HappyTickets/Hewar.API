﻿using Application.Tickets.Dtos;
using Application.Tickets.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/tickets")]
    public class TicketsController : ApiControllerBase
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        [HttpPost("createTicket")]
        [HasPermission(Permissions.CreateTicket)]
        public async Task<IActionResult> CreateTicketAsync(CreateTicketDto dto)
            => Result(await _ticketsService.CreateTicketAsync(dto));

        [HttpPost("createMessage")]
        [HasPermission(Permissions.CreateTicketMessage)]
        public async Task<IActionResult> CreateMessageAsync(CreateTicketMessageDto dto)
            => Result(await _ticketsService.CreateMessageAsync(dto));

        [HttpPatch("closeTicket")]
        [HasPermission(Permissions.CloseTicket)]
        public async Task<IActionResult> CloseTicketAsync(long ticketId)
           => Result(await _ticketsService.CloseTicketAsync(ticketId));

        [HttpGet("getTickets")]
        [HasPermission(Permissions.ViewTickets)]
        public async Task<IActionResult> GetTicketsAsync(long priceRequestId)
            => Result(await _ticketsService.GetTicketsAsync(priceRequestId));

        [HttpGet("getMessages")]
        [HasPermission(Permissions.ViewTicketMessages)]
        public async Task<IActionResult> GetMessagesAsync(long ticketId)
            => Result(await _ticketsService.GetMessagesAsync(ticketId));
    }
}
