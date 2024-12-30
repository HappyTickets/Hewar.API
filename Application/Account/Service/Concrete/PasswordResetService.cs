using Application.AccountManagement.Dtos.Password;
using Application.AccountManagement.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Web;
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
        await _emailSender.SendAsync(user.Email!, Resource.Password_Reset,
            string.Format(Resource.Password_Reset_Message, token));
        
        return Empty.Default;
    }

    public async Task<Result<Empty>> ResetPasswordAsync(ResetPasswordTokenDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null) return new NotFoundError();

        var resetResult = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        return ProcessIdentityResult(resetResult);
    }

    public async Task<Result<string>> ResetPasswordAsync(ChangePasswordRequest resetPasswordRequest, CancellationToken cancellationToken = default)
    {

        var user = await _userManager.FindByIdAsync(_currentUser.Id.ToString());

        if (user is null)
        {
            return new UnauthorizedError(ErrorCodes.UnregisteredEmail, Resource.Credentials_Invalid);
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, resetPasswordRequest.OldPassword, resetPasswordRequest.NewPassword);

        if (!changePasswordResult.Succeeded || changePasswordResult.Errors.Any())
        {
            return new ValidationError(changePasswordResult.Errors.Select(e => e.Description).ToList());
        }

        return Resource.Password_Reset_Succeed;

    }

    private Result<Empty> ProcessIdentityResult(IdentityResult result)
    {
        return result.Succeeded ? Empty.Default : new ValidationError(result.Errors.Select(e => e.Description));
    }
}
