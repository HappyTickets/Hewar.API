using Application.Authorization.DTOs.Request;
using Application.Authorization.Service;
using Application.Common.Utilities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AuthorizationController : ApiControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("create")]
        //[HasPermission(Permissions.CreateRole)]
        public async Task<IActionResult> CreateAsync([FromBody] AddRoleDto addRoleDto)
            => Result(await _authorizationService.AddRoleAsync(addRoleDto));

        [HttpPut("update")]
        //[HasPermission(Permissions.UpdateRole)]
        public async Task<IActionResult> EditAsync([FromBody] EditRoleDto editRoleDto)
            => Result(await _authorizationService.EditRoleAsync(editRoleDto));

        [HttpDelete("delete/{id:long}")]
        //[HasPermission(Permissions.DeleteRole)]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
            => Result(await _authorizationService.DeleteRoleAsync(id));

        [HttpGet("getAll")]
        //[HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetAllAsync()
            => Result(await _authorizationService.GetRolesList());

        [HttpGet("getById/{id:long}")]
        //[HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
            => Result(await _authorizationService.GetRoleById(id));

        [HttpPost("assign-users")]
        //[HasPermission(Permissions.AssignUserToRole)]
        public async Task<IActionResult> AssignUsersToRoleAsync([FromBody] AssignUsersToRoleDto assignUsersToRoleDto, CancellationToken cancellationToken = default)
            => Result(await _authorizationService.AssignUsersToRoleAsync(assignUsersToRoleDto, cancellationToken));

        [HttpPost("assign-user-to-roles")]
        //[HasPermission(Permissions.AssignUserToRole)]
        public async Task<IActionResult> AssignUserToRolesAsync([FromBody] AssignUserToRolesDto assignUserToRoleDto)
            => Result(await _authorizationService.AssignUserToRolesAsync(assignUserToRoleDto));

        [HttpPost("remove-users")]
        //[HasPermission(Permissions.UnassignUserToRole)]
        public async Task<IActionResult> RemoveUsersFromRoleAsync([FromBody] RemoveUsersFromRoleDto removeUserFromRoleDto, CancellationToken cancellationToken = default)
            => Result(await _authorizationService.RemoveUsersFromRoleAsync(removeUserFromRoleDto, cancellationToken));

        [HttpGet("users/{userId:long}/roles")]
        public async Task<IActionResult> GetUserWithRolesAsync([FromRoute] long userId)
            => Result(await _authorizationService.GetUserWithRolesAsync(userId));

        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUsersWithRolesAsync([FromQuery] PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken = default)
            => Result(await _authorizationService.GetUsersWithRolesAsync(paginationSearchModel, cancellationToken));

        [HttpGet("roles/{roleId:long}/users")]
        //[HasPermission(Permissions.ViewRoles)]
        public async Task<IActionResult> GetRoleWithUsersAsync([FromRoute] long roleId, [FromQuery] PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken)
            => Result(await _authorizationService.GetRoleWithUsersAsync(roleId, paginationSearchModel, cancellationToken));
    }
}
