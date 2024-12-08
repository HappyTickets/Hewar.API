using Application.Authorization.DTOs.Request;
using Application.Authorization.DTOs.Response;
using Application.Common.Utilities.Pagination;

namespace Application.Authorization.Service
{
    public interface IAuthorizationService
    {
        Task<Result<Empty>> AddRoleAsync(AddRoleDto addRoleDto);
        Task<bool> IsRoleExistById(long roleId);
        Task<bool> IsRoleExist(string roleName);
        Task<Result<Empty>> EditRoleAsync(EditRoleDto request);
        Task<Result<Empty>> DeleteRoleAsync(long roleId);
        Task<Result<List<RoleDto>>> GetRolesList(CancellationToken cancellationToken = default);
        Task<Result<RoleDto>> GetRoleById(long id);

        Task<Result<Empty>> AssignUserToRolesAsync(AssignUserToRolesDto assignUserToRolesDto);
        Task<Result<Empty>> AssignUsersToRoleAsync(AssignUsersToRoleDto assignUsersToRoleDto, CancellationToken cancellationToken = default);
        Task<Result<Empty>> RemoveUsersFromRoleAsync(RemoveUsersFromRoleDto removeUsersFromRoleDto, CancellationToken cancellationToken = default);

        Task<Result<UserWithRolesDto>> GetUserWithRolesAsync(long userId);
        Task<Result<RoleWithUsersDto>> GetRoleWithUsersAsync(long roleId, PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken = default);
        Task<Result<PaginatedList<UserWithRolesDto>>> GetUsersWithRolesAsync(PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken);
    }

}