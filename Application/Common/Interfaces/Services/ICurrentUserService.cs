namespace Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        long? AccountId { get; }
        long? IdentityId { get; }
        string? Email { get; }
        AccountTypes? Type { get; }
    }
}
