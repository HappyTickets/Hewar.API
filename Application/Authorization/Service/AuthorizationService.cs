using Application.AccountManagement.Dtos.User;
using Application.Authorization.DTOs.Request;
using Application.Authorization.DTOs.Response;
using Application.Common.Utilities.Pagination;
using AutoMapper;
using Domain.Entities.UserEntities;
using LanguageExt;
using Localization.ResourceFiles;
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
            };

            var result = await _roleManager.CreateAsync(identityRole);

            if (!result.Succeeded)
            {
                var addRoleFailedExp = new ValidationException(result.Errors.Select(e => e.Description).ToList());

                return addRoleFailedExp;
            }
            return new();
        }


        public async Task<Result<Empty>> DeleteRoleAsync(long roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role?.Name is null)
            {
                var notFoundExp = new NotFoundException(Resource.RoleDeletionFailed);
                return notFoundExp;
            }

            // Delete the role
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Empty.Default;
            }

            var deleteFailedExp = new ValidationException(result.Errors.Select(e => e.Description).ToList());

            return deleteFailedExp;
        }

        public async Task<Result<Empty>> EditRoleAsync(EditRoleDto editRoleDto)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == editRoleDto.RoleId);

            if (role == null)
                return new NotFoundException(Resource.NotFound);

            if (editRoleDto.RoleName != null)
            {
                role.Name = editRoleDto.RoleName;
            }
            role.Description = editRoleDto.RoleDescription;


            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
                return new ValidationException(result.Errors.Select(e => e.Description));


            return new();
        }

        public async Task<Result<RoleDto>> GetRoleById(long id)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return new NotFoundException(Resource.GetRoleFailed);
            }

            var roleDto = _mapper.Map<RoleDto>(role);
            return roleDto;
        }

        public async Task<Result<List<RoleDto>>> GetRolesList(CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
            var rolesDto = _mapper.Map<List<RoleDto>>(roles);
            return rolesDto;
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> IsRoleExistById(long roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            return role != null;
        }

        #endregion

        #region User-Role Assigning Operations

        public async Task<Result<Empty>> AssignUserToRolesAsync(AssignUserToRolesDto assignUserToRolesDto)
        {

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(assignUserToRolesDto.UserId.ToString());
            if (user == null)
            {
                return new NotFoundException(Resource.NotFound);
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
                    return new ValidationException(removeResult.Errors.Select(e => e.Description));
                }
            }

            // Assign new roles if any
            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    return new ForbiddenException(Resource.AssignRoleFailed);
                }
            }
            return Empty.Default;

        }

        public async Task<Result<Empty>> AssignUsersToRoleAsync(AssignUsersToRoleDto assignUsersToRoleDto, CancellationToken cancellationToken = default)
        {

            // Validate input
            if (assignUsersToRoleDto is null || assignUsersToRoleDto.RoleId == 0 || !assignUsersToRoleDto.UserIds.Any())
            {
                return new ValidationException(Resource.AssignRoleFailed);
            }

            return await _authorizationRepository.AssignUsersToRoleAsync(assignUsersToRoleDto, cancellationToken);

        }

        public async Task<Result<Empty>> RemoveUsersFromRoleAsync(RemoveUsersFromRoleDto removeUsersFromRoleDto, CancellationToken cancellationToken = default)
        {

            if (removeUsersFromRoleDto == null || removeUsersFromRoleDto.RoleId == 0 || !removeUsersFromRoleDto.UserIds.Any())
            {
                return new ForbiddenException(Resource.RemoveRoleFailed);
            }

            return await _authorizationRepository.UnassignUsersFromRoleAsync(removeUsersFromRoleDto, cancellationToken);

        }

        public async Task<Result<RoleWithUsersDto>> GetRoleWithUsersAsync(long roleId, PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role?.Name is null)
            {
                return new NotFoundException(Resource.GetRoleFailed);
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

            return roleWithUsersDto;

        }

        public async Task<Result<UserWithRolesDto>> GetUserWithRolesAsync(long userId)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return new NotFoundException(Resource.NotFoundInDB_Message);
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

            return userWithRolesDto;

        }

        public async Task<Result<PaginatedList<UserWithRolesDto>>> GetUsersWithRolesAsync(PaginationSearchModel paginationSearchModel, CancellationToken cancellationToken)
        {

            var filteredUsersQuery = SearchFilterPagination.Filter(_userManager.Users, paginationSearchModel);

            var totalItems = await filteredUsersQuery.CountAsync(cancellationToken);

            var items = _authorizationRepository.GetUsersWithRolesAsync(filteredUsersQuery, cancellationToken);

            return new PaginatedList<UserWithRolesDto>(items, totalItems, paginationSearchModel.PageIndex, paginationSearchModel.PageSize);

        }
        #endregion
    }
}