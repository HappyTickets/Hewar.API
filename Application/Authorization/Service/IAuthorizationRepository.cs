using Application.Authorization.DTOs.Request;
using Application.Authorization.DTOs.Response;
using Domain.Entities.UserEntities;

namespace Application.Authorization.Service
{
    public interface IAuthorizationRepository
    {
        Task<Empty> UnassignUsersFromRoleAsync(RemoveUsersFromRoleDto dto, CancellationToken cancellationToken = default);
        IQueryable<ApplicationUser> GetUsersInRole(long roleId);

        IEnumerable<UserWithRolesDto> GetUsersWithRolesAsync(IQueryable<ApplicationUser> users, CancellationToken cancellationToken);
        Task<Empty> AssignUsersToRoleAsync(AssignUsersToRoleDto dto, CancellationToken cancellationToken = default);
    }
}
