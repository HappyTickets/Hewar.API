using Application.Account.OTP.Extensions;
using Application.AccountManagement.Dtos.Email;
using Application.AccountManagement.OTP.Extensions;
using Application.AccountManagement.Service.Interfaces;
using Microsoft.AspNetCore.Http;
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
        if (user is null) return new NotFoundError(ErrorCodes.UnregisteredEmail);

        var result = await SendConfirmationEmailAsync(user, cancellationToken);
        return result.IsSuccess
            ? new Result<Empty>
            {
                Status = StatusCodes.Status200OK,
                IsSuccess = true,
                SuccessCode = SuccessCodes.EmailConfirmation,
                Data = Empty.Default
            }
            : result;
    }
    private async Task<Result<Empty>> SendConfirmationEmailAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        var tokenOtp = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var subject = EnumExtension.GetDisplayName(SuccessCodes.EmailConfirmation);
        await _emailSender.SendAsync(user.Email!, subject,
            SuccessCodes.EmailConfirmationMessage, tokenOtp);

        return Empty.Default;
    }
    public async Task<Result<Empty>> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return new NotFoundError(ErrorCodes.UnregisteredEmail);

        var confirmationResults = await _userManager.ConfirmEmailAsync(user, request.VerificationCode);
        return ProcessIdentityResult(confirmationResults, SuccessCodes.EmailConfirmed);
    }
    public async Task<Result<Empty>> ConfirmChangeEmailAsync(ConfirmEmailRequest confirmEmailRequest, CancellationToken cancellationToken = default)
    {

        var user = await _userManager.FindByIdAsync(_currentUser.UserId.ToString());
        if (user is null) return new NotFoundError(ErrorCodes.UserNotExists);

        var tokenOtp = await _userManager.GenerateOtpTokenAsync("ChangeEmail", user);

        if (string.Equals(tokenOtp, confirmEmailRequest.VerificationCode, StringComparison.InvariantCultureIgnoreCase))
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, confirmEmailRequest.Email);
            var res = await _userManager.ChangeEmailAsync(user, confirmEmailRequest.Email, token);
            return ProcessIdentityResult(res, SuccessCodes.EmailChangedSuccessfully);

        }

        return new UnauthorizedError();
    }
    private Result<Empty> ProcessIdentityResult(IdentityResult result, SuccessCodes successCode)
    {
        return result.Succeeded
            ? new Result<Empty>
            {
                Status = StatusCodes.Status200OK,
                IsSuccess = true,
                SuccessCode = successCode,
                Data = Empty.Default
            }
            : new ValidationError(result.Errors.Select(e => e.Description));
    }
}
