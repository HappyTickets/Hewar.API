using Application.AccountManagement.Dtos.Email;
using Application.AccountManagement.OTP.Extensions;
using Application.AccountManagement.Service.Interfaces;
using Domain.Entities.UserEntities;
using Localization.ResourceFiles;
using Microsoft.AspNetCore.Identity;
namespace Application.AccountManagement.Service.Concrete;
public class EmailConfirmationService(UserManager<ApplicationUser> userManager, IEmailSender emailSender, ICurrentUserService currentUser) : IEmailConfirmationService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<Result<Empty>> SendEmailConfirmationAsync(SendEmailConfirmationRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return new NotFoundException(Resource.Email_NotFound);

        return await SendConfirmationEmailAsync(user, cancellationToken);
    }


    private async Task<Result<Empty>> SendConfirmationEmailAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        var tokenOtp = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return await _emailSender.SendEmailAsync(user.Email!, Resource.Email_Confirmation,
            string.Format(Resource.Email_Confirmation_Message, tokenOtp), cancellationToken: cancellationToken);
    }
    public async Task<Result<Empty>> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return new NotFoundException(Resource.NotFoundInDB_Message);

        var confirmationResults = await _userManager.ConfirmEmailAsync(user, request.VerificationCode);
        return ProcessIdentityResult(confirmationResults);
    }


    public async Task<Result<Empty>> ConfirmChangeEmailAsync(ConfirmEmailRequest confirmEmailRequest, CancellationToken cancellationToken = default)
    {

        var user = await _userManager.FindByIdAsync(_currentUser.Id.ToString());
        if (user is null) return new NotFoundException(Resource.NotFoundInDB_Message);

        var tokenOtp = await _userManager.GenerateOtpTokenAsync("ChangeEmail", user);

        if (string.Equals(tokenOtp, confirmEmailRequest.VerificationCode, StringComparison.InvariantCultureIgnoreCase))
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, confirmEmailRequest.Email);
            var res = await _userManager.ChangeEmailAsync(user, confirmEmailRequest.Email, token);
            return ProcessIdentityResult(res);

        }

        return new UnauthorizedException();
    }
    private Result<Empty> ProcessIdentityResult(IdentityResult result)
    {
        return result.Succeeded
            ? new Result<Empty>()
            : new ValidationException(result.Errors.Select(e => e.Description));
    }
}
