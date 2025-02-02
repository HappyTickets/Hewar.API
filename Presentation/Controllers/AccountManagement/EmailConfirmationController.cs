using Application.AccountManagement.Dtos.Email;
using Application.AccountManagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.AccountManagement;
//[Authorize]

public class EmailConfirmationController : ApiControllerBase
{
    private readonly IEmailConfirmationService _emailConfirmationService;

    public EmailConfirmationController(IEmailConfirmationService emailConfirmationService)
    {
        _emailConfirmationService = emailConfirmationService;
    }

    [HttpPost("send-confirmation")]
    [AllowAnonymous]
    public async Task<IActionResult> SendEmailConfirmation([FromBody] SendEmailConfirmationRequest request, CancellationToken cancellationToken = default)
    {

        return Result(await _emailConfirmationService.SendEmailConfirmationAsync(request, cancellationToken));
    }

    [HttpPost("confirm")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken = default)
    {
        return Result(await _emailConfirmationService.ConfirmEmailAsync(request, cancellationToken));
    }

    //[HttpPost("send-change-confirmation")]
    //public async Task<IActionResult> SendEmailChangeConfirmation([FromBody] ChangeEmailRequest request, CancellationToken cancellationToken = default)
    //{
    //    return Result(await _emailConfirmationService.SendChangeEmailConfirmationAsync(request, cancellationToken));
    //}

    [HttpPost("confirm-change")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmChangeEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken = default)
    {
        return Result(await _emailConfirmationService.ConfirmChangeEmailAsync(request, cancellationToken));
    }
}


