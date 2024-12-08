using Application.AccountManagement.Dtos.Password;

namespace Application.AccountManagement.Service.Interfaces;

public interface IPasswordResetService
{

    Task<Result<Empty>> CreatePasswordResetTokenAsync(CreatePasswordResetTokenRequest request, CancellationToken cancellationToken = default);
    Task<Result<Empty>> ResetPasswordAsync(ResetPasswordTokenDto dto, CancellationToken cancellationToken = default);
    Task<Result<string>> ResetPasswordAsync(ChangePasswordRequest resetPasswordRequest, CancellationToken cancellationToken = default);
}


