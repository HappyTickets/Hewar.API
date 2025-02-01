namespace Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        long? UserId { get; }
        long? EntityId { get; } // company id, facility id
        EntityTypes? EntityType { get; } // company, facility
        string? Email { get; }
    }
}
