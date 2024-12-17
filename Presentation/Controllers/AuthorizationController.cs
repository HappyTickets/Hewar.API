using Application.Authorization.DTOs.Request;
using Application.Authorization.Service;
using Application.Common.Utilities.Pagination;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace API.Controllers
{
    public class AuthorizationController(IAuthorizationService authorizationService) : ApiControllerBase
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [HttpPost("roles/create")]
        [HasPermission(Permissions.CreateRole)]
        public async Task<IActionResult> Create([FromBody] AddRoleDto addRoleDto)
        {
            return Result(await _authorizationService.AddRoleAsync(addRoleDto));
        }

        [HttpPut("roles/edit")]
        [HasPermission(Permissions.UpdateRole)]
        public async Task<IActionResult> Edit([FromBody] EditRoleDto editRoleDto)
        {
            return Result(await _authorizationService.EditRoleAsync(editRoleDto));
        }

        [HttpDelete("roles/{id:long}")]
        [HasPermission(Permissions.DeleteRole)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            return Result(await _authorizationService.DeleteRoleAsync(id));
        }

        [HttpGet("roles")]
        //[HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetRolesList()
        {
            return Result(await _authorizationService.GetRolesList());
        }

        [HttpGet("roles/{id:long}")]
        [HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetRoleById([FromRoute] long id)
        {
            return Result(await _authorizationService.GetRoleById(id));
        }

        [HttpPost("roles/assign-users")]
        [HasPermission(Permissions.AssignUserToRole)]
        public async Task<IActionResult> AssignUsersToRole([FromBody] AssignUsersToRoleDto assignUsersToRoleDto, CancellationToken cancellationToken = default)
        {
            return Result(await _authorizationService.AssignUsersToRoleAsync(assignUsersToRoleDto, cancellationToken));
        }

        [HttpPost("roles/assign-user-to-roles")]
        [HasPermission(Permissions.AssignUserToRole)]
        public async Task<IActionResult> AssignUserToRoles([FromBody] AssignUserToRolesDto assignUserToRoleDto)
        {
            return Result(await _authorizationService.AssignUserToRolesAsync(assignUserToRoleDto));
        }

        [HttpPost("roles/remove-users")]
        [HasPermission(Permissions.UnassignUserToRole)]
        public async Task<IActionResult> RemoveUsersFromRole([FromBody] RemoveUsersFromRoleDto removeUserFromRoleDto, CancellationToken cancellationToken = default)
        {
            return Result(await _authorizationService.RemoveUsersFromRoleAsync(removeUserFromRoleDto, cancellationToken));
        }

        [HttpGet("users/{userId:long}/roles")]
        [HasPermission(Permissions.ViewUsers)]
        public async Task<IActionResult> GetUserWithRoles([FromRoute] long userId)
        {
            return Result(await _authorizationService.GetUserWithRolesAsync(userId));
        }

        [HttpGet("users-with-roles")]
        [HasPermission(Permissions.ViewUsers)]
        public async Task<IActionResult> GetUsersWithRoles([FromQuery] PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken = default)
        {
            return Result(await _authorizationService.GetUsersWithRolesAsync(paginationSearchModel, cancellationToken));
        }

        [HttpGet("roles/{roleId:long}/users")]
        [HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetRoleWithUsers([FromRoute] long roleId, [FromQuery] PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.GetRoleWithUsersAsync(roleId, paginationSearchModel, cancellationToken);
            return Result(result);
        }
    }
}
