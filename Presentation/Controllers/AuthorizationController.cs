using Application.Authorization.DTOs.Request;
using Application.Authorization.Service;
using Application.Common.Utilities.Pagination;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AuthorizationController(IAuthorizationService authorizationService) : ApiControllerBase
    {
        [HttpPost("create")]
        [HasPermission(Permissions.CreateRole)]
        public async Task<IActionResult> CreateAsync([FromBody] AddRoleDto addRoleDto)
            => Result(await authorizationService.AddRoleAsync(addRoleDto));

        [HttpPut("update")]
        [HasPermission(Permissions.UpdateRole)]
        public async Task<IActionResult> EditAsync([FromBody] EditRoleDto editRoleDto)
            => Result(await authorizationService.EditRoleAsync(editRoleDto));

        [HttpDelete("delete/{id:long}")]
        [HasPermission(Permissions.DeleteRole)]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
            => Result(await authorizationService.DeleteRoleAsync(id));

        [HttpGet("getAll")]
        [HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await authorizationService.GetRolesList());

        [HttpGet("getById/{id:long}")]
        [HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
            => Result(await authorizationService.GetRoleById(id));

        [HttpPost("assign-users")]
        [HasPermission(Permissions.AssignUserToRole)]
        public async Task<IActionResult> AssignUsersToRoleAsync([FromBody] AssignUsersToRoleDto assignUsersToRoleDto, CancellationToken cancellationToken = default)
            => Result(await authorizationService.AssignUsersToRoleAsync(assignUsersToRoleDto, cancellationToken));

        [HttpPost("assign-user-to-roles")]
        [HasPermission(Permissions.AssignUserToRole)]
        public async Task<IActionResult> AssignUserToRolesAsync([FromBody] AssignUserToRolesDto assignUserToRoleDto)
            => Result(await authorizationService.AssignUserToRolesAsync(assignUserToRoleDto));

        [HttpPost("remove-users")]
        [HasPermission(Permissions.UnassignUserToRole)]
        public async Task<IActionResult> RemoveUsersFromRoleAsync([FromBody] RemoveUsersFromRoleDto removeUserFromRoleDto, CancellationToken cancellationToken = default)
            => Result(await authorizationService.RemoveUsersFromRoleAsync(removeUserFromRoleDto, cancellationToken));

        [HttpGet("users/{userId:long}/roles")]
        [HasPermission(Permissions.ViewUsers)]
        public async Task<IActionResult> GetUserWithRolesAsync([FromRoute] long userId)
            => Result(await authorizationService.GetUserWithRolesAsync(userId));

        [HttpGet("users-with-roles")]
        [HasPermission(Permissions.ViewUsers)]
        public async Task<IActionResult> GetUsersWithRolesAsync([FromQuery] PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken = default)
            => Result(await authorizationService.GetUsersWithRolesAsync(paginationSearchModel, cancellationToken));

        [HttpGet("roles/{roleId:long}/users")]
        [HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetRoleWithUsersAsync([FromRoute] long roleId, [FromQuery] PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken)
            => Result(await authorizationService.GetRoleWithUsersAsync(roleId, paginationSearchModel, cancellationToken));
    }
}
