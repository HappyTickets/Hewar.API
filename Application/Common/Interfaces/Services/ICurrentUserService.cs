namespace Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        long? UserId { get; }
        string? Email { get; }
    }
}
