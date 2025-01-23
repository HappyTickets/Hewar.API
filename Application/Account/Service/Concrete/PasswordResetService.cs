using Application.Account.OTP.Extensions;
using Application.AccountManagement.Dtos.Password;
using Application.AccountManagement.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
namespace Application.AccountManagement.Service.Concrete;
public class PasswordResetService : IPasswordResetService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUser;
    private readonly IEmailSender _emailSender;

    public PasswordResetService(UserManager<ApplicationUser> userManager, ICurrentUserService currentUser, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _currentUser = currentUser;
    }

    public async Task<Result<Empty>> CreatePasswordResetTokenAsync(CreatePasswordResetTokenRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return new NotFoundError();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var subject = EnumExtension.GetDisplayName(SuccessCodes.PasswordReset);
        await _emailSender.SendAsync(user.Email!, subject,
            SuccessCodes.PasswordResetMessage, token);

        return Result<Empty>.Success(Empty.Default, SuccessCodes.PasswordResetMessage);

    }

    public async Task<Result<Empty>> ResetPasswordAsync(ResetPasswordTokenDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null) return new NotFoundError();

        var resetResult = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        return ProcessIdentityResult(resetResult, SuccessCodes.PasswordReset);
    }
    public async Task<Result<SuccessCodes>> ResetPasswordAsync(ChangePasswordRequest resetPasswordRequest, CancellationToken cancellationToken = default)
    {

        var user = await _userManager.FindByIdAsync(_currentUser.IdentityId.ToString());

        if (user is null)
        {
            return new UnauthorizedError(ErrorCodes.UnregisteredEmail);
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, resetPasswordRequest.OldPassword, resetPasswordRequest.NewPassword);

        if (!changePasswordResult.Succeeded || changePasswordResult.Errors.Any())
        {
            return new ValidationError(changePasswordResult.Errors.Select(e => e.Description).ToList());
        }

        return SuccessCodes.PasswordReset;

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
