using Application.AccountManagement.Dtos.Email;


namespace Application.AccountManagement.Service.Interfaces;


public interface IEmailConfirmationService
{

    Task<Result<Empty>> SendEmailConfirmationAsync(SendEmailConfirmationRequest request, CancellationToken cancellationToken = default);
    Task<Result<Empty>> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default);

    Task<Result<Empty>> ConfirmChangeEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default);
}

