using Application.Authorization.DTOs.Request;
using Application.Authorization.DTOs.Response;
using Application.Authorization.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class AuthorizationRepository(UserManager<ApplicationUser> userManager, AppDbContext context) : IAuthorizationRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Empty> UnassignUsersFromRoleAsync(RemoveUsersFromRoleDto dto, CancellationToken cancellationToken = default)
        {

            var userRoles = await _context.UserRoles
                .Where(ur => ur.RoleId == dto.RoleId && dto.UserIds.Contains(ur.UserId))
                .ToListAsync(cancellationToken);

            _context.UserRoles.RemoveRange(userRoles);

            await _context.SaveChangesAsync(cancellationToken);

            return Empty.Default; // Return success result

        }

        public async Task<Empty> AssignUsersToRoleAsync(AssignUsersToRoleDto dto, CancellationToken cancellationToken = default)
        {

            var newUserRoles = new List<ApplicationUserRole>();

            foreach (var userId in dto.UserIds)
            {
                var existingAssignment = await _context.UserRoles
                    .AnyAsync(ur => ur.RoleId == dto.RoleId && ur.UserId == userId, cancellationToken);

                if (!existingAssignment)
                {
                    newUserRoles.Add(new()
                    {
                        UserId = userId,
                        RoleId = dto.RoleId
                    });
                }
            }

            // Bulk insert all new user-role mappings
            await _context.UserRoles.AddRangeAsync(newUserRoles, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Empty.Default;// Return success result
        }

        public IQueryable<ApplicationUser> GetUsersInRole(long roleId)
        {
            var usersRoleQuery = _context.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .Select(ur => ur.UserId);

            var usersInRoleQuery = _context.Users.Where(u => usersRoleQuery.Contains(u.Id));

            return usersInRoleQuery;
        }

        public IEnumerable<UserWithRolesDto> GetUsersWithRolesAsync(IQueryable<ApplicationUser> users, CancellationToken cancellationToken)
        {
            try
            {
                var query = from user in users
                            join userRole in _context.UserRoles on user.Id equals userRole.UserId into userRolesGroup
                            from userRole in userRolesGroup.DefaultIfEmpty() // Left join
                            join role in _context.Roles on userRole.RoleId equals role.Id into rolesGroup
                            from role in rolesGroup.DefaultIfEmpty() // Left join
                            group role by new { user.Id, user.UserName, user.Email } into userGroup
                            select new UserWithRolesDto
                            {
                                UserId = userGroup.Key.Id,
                                UserName = userGroup.Key.UserName,
                                Email = userGroup.Key.Email,
                                AssignedRoles = userGroup.Where(r => r != null).Select(r => r.Name).ToList() // Handle null roles
                            };

                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
