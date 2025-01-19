using Application.AccountManagement.Dtos.User;
using Application.Authorization.DTOs.Request;
using Application.Authorization.DTOs.Response;
using Application.Common.Utilities.Pagination;
using AutoMapper;
using Domain.Enums;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authorization.Service
{
    public class AuthorizationService(RoleManager<ApplicationRole> roleManager,
                                      UserManager<ApplicationUser> userManager,
                                      IMapper mapper,
                                      IAuthorizationRepository authorizationRepository) : IAuthorizationService
    {
        #region Fields
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorizationRepository _authorizationRepository = authorizationRepository;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        #endregion

        #region Role Operations
        public async Task<Result<Empty>> AddRoleAsync(AddRoleDto addRoleDto)
        {

            var identityRole = new ApplicationRole
            {
                Name = addRoleDto.RoleName,
                Description = addRoleDto.RoleDescription,
                Permissions = addRoleDto.Permissions.Select(p => new RolePermission { Permission = p }).ToList()
            };

            var result = await _roleManager.CreateAsync(identityRole);

            if (!result.Succeeded)
            {
                var addRoleFailedExp = new ValidationError(result.Errors.Select(e => e.Description).ToList());

                return addRoleFailedExp;
            }

            return Result<Empty>.Success(Empty.Default, SuccessCodes.RoleCreated);

        }


        public async Task<Result<Empty>> DeleteRoleAsync(long roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role?.Name is null)
            {
                var notFoundExp = new NotFoundError(ErrorCodes.RoleNotExists);
                return notFoundExp;
            }

            // Delete the role
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Result<Empty>.Success(Empty.Default, SuccessCodes.RoleDeleted);

            }

            var deleteFailedExp = new ValidationError(result.Errors.Select(e => e.Description).ToList());

            return deleteFailedExp;

        }

        public async Task<Result<Empty>> EditRoleAsync(EditRoleDto editRoleDto)
        {
            var role = await _roleManager.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == editRoleDto.RoleId);

            if (role == null)
                return new NotFoundError(ErrorCodes.RoleNotExists);

            if (editRoleDto.RoleName != null)
            {
                role.Name = editRoleDto.RoleName;
            }

            role.Description = editRoleDto.RoleDescription;
            role.Permissions = editRoleDto.Permissions.Select(p => new RolePermission { Permission = p }).ToList();

            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
                return new ValidationError(result.Errors.Select(e => e.Description));



            return Result<Empty>.Success(Empty.Default, SuccessCodes.RoleUpdated);

        }

        public async Task<Result<RoleDto>> GetRoleById(long id)
        {
            var role = await _roleManager.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return new NotFoundError(ErrorCodes.GetRoleFaild);
            }

            var roleDto = _mapper.Map<RoleDto>(role);

            return Result<RoleDto>.Success(roleDto, SuccessCodes.RoleReceived);

        }

        public async Task<Result<List<RoleDto>>> GetRolesList(CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles
                .Include(r => r.Permissions)
                .ToListAsync(cancellationToken);
            var rolesDto = _mapper.Map<List<RoleDto>>(roles);

            return Result<List<RoleDto>>.Success(rolesDto, SuccessCodes.RolesReceived);

        }

        public async Task<Result<bool>> IsRoleExist(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            return Result<bool>.Success(roleExists, SuccessCodes.RoleExists);
        }

        public async Task<Result<bool>> IsRoleExistById(long roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            return Result<bool>.Success(role != null, SuccessCodes.RoleExists);
        }

        #endregion

        #region User-Role Assigning Operations

        public async Task<Result<Empty>> AssignUserToRolesAsync(AssignUserToRolesDto assignUserToRolesDto)
        {

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(assignUserToRolesDto.UserId.ToString());
            if (user == null)
            {
                return new NotFoundError(ErrorCodes.UserNotExists);
            }

            // Fetch current roles of the user
            var currentRoles = await _userManager.GetRolesAsync(user);

            var rolesToRemove = currentRoles.Except(assignUserToRolesDto.Roles).ToList();
            var rolesToAdd = assignUserToRolesDto.Roles.Except(currentRoles).ToList();

            // Remove roles if necessary
            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    return new ValidationError(removeResult.Errors.Select(e => e.Description));
                }
            }

            // Assign new roles if any
            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    return new ForbiddenError(ErrorCodes.AssignUserRoleFailed);
                }
            }

            return Result<Empty>.Success(Empty.Default, SuccessCodes.RoleAssigned);

        }

        public async Task<Result<Empty>> AssignUsersToRoleAsync(AssignUsersToRoleDto assignUsersToRoleDto, CancellationToken cancellationToken = default)
        {

            // Validate input
            if (assignUsersToRoleDto is null || assignUsersToRoleDto.RoleId == 0 || !assignUsersToRoleDto.UserIds.Any())
            {
                return new ValidationError(ErrorCodes.AssignUserRoleFailed);
            }

            await _authorizationRepository.AssignUsersToRoleAsync(assignUsersToRoleDto, cancellationToken);
            return Result<Empty>.Success(Empty.Default, SuccessCodes.RoleAssigned);

        }

        public async Task<Result<Empty>> RemoveUsersFromRoleAsync(RemoveUsersFromRoleDto removeUsersFromRoleDto, CancellationToken cancellationToken = default)
        {

            if (removeUsersFromRoleDto == null || removeUsersFromRoleDto.RoleId == 0 || !removeUsersFromRoleDto.UserIds.Any())
            {
                return new ForbiddenError(ErrorCodes.RemoveRoleFailed);
            }

            await _authorizationRepository.UnassignUsersFromRoleAsync(removeUsersFromRoleDto, cancellationToken);
            return Result<Empty>.Success(Empty.Default, SuccessCodes.UserRemovedFromRole);

        }

        public async Task<Result<RoleWithUsersDto>> GetRoleWithUsersAsync(long roleId, PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role?.Name is null)
            {
                return new NotFoundError(ErrorCodes.GetRoleFaild);
            }

            // Use repository to fetch users and apply pagination
            var usersInRoleQuery = _authorizationRepository.GetUsersInRole(roleId);
            var filterdUsers = SearchFilterPagination.Filter(usersInRoleQuery, paginationSearchModel);
            var usersCount = await filterdUsers.CountAsync();
            var paginatedUserList = await SearchFilterPagination.PaginateData(filterdUsers, paginationSearchModel);

            // Map the paginated users to DTO
            var roleAppUsers = paginatedUserList.Select(_mapper.Map<ApplicationUserDTO>).ToList();

            var roleWithUsersDto = new RoleWithUsersDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Description = role.Description,
                AssignedUsers = new(roleAppUsers, usersCount, paginationSearchModel.PageIndex, paginationSearchModel.PageSize),
            };

            return Result<RoleWithUsersDto>.Success(roleWithUsersDto, SuccessCodes.RoleAssigned);

        }

        public async Task<Result<UserWithRolesDto>> GetUserWithRolesAsync(long userId)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return new NotFoundError(ErrorCodes.NotFound);
            }

            // Get the roles assigned to this user
            var rolesNames = await _userManager.GetRolesAsync(user);

            // Map the user and assigned roles to the DTO
            var userWithRolesDto = new UserWithRolesDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                AssignedRoles = rolesNames.ToList()
            };

            return Result<UserWithRolesDto>.Success(userWithRolesDto, SuccessCodes.UserReceived);


        }

        public async Task<Result<PaginatedList<UserWithRolesDto>>> GetUsersWithRolesAsync(PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken)
        {

            var filteredUsersQuery = SearchFilterPagination.Filter(_userManager.Users, paginationSearchModel);

            var totalItems = await filteredUsersQuery.CountAsync(cancellationToken);

            var items = _authorizationRepository.GetUsersWithRolesAsync(filteredUsersQuery, cancellationToken);

            var usersWithRoles = new PaginatedList<UserWithRolesDto>(items, totalItems, paginationSearchModel.PageIndex, paginationSearchModel.PageSize);
            return Result<PaginatedList<UserWithRolesDto>>.Success(usersWithRoles, SuccessCodes.UserReceived);

        }
        #endregion
    }
}